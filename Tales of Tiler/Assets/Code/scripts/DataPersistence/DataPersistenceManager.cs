using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

// Applying Singleton Strategy
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    private string _selectedProfileId = "";
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying newest one");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        _dataPersistenceObjects = FindAllDataPersistences();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _dataPersistenceObjects = FindAllDataPersistences();
        LoadGame();
    }
    
    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load(_selectedProfileId);
        
        //if no data can be loaded, initialize as a new game
        if (_gameData == null)
        {
            Debug.Log("No data was found. A new game must be started before data can be loaded.");
            return;
        }
        
        foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
        {
            dataPersistence.LoadData(_gameData);
        }
    }
    
    public void SaveGame()
    {

        if (_gameData == null)
        {
            Debug.LogWarning("No data was found. A new game must be started before data can be saved");
            return;
        }
        
        foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref _gameData);
        }
        
        _dataHandler.Save(_gameData, _selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    
    public void ChangeSelectedProfileId(string newProfileId)
    {
        _selectedProfileId = newProfileId;
        LoadGame();
    }
    
    private List<IDataPersistence> FindAllDataPersistences()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _dataHandler.LoadAllProfiles();
    }
}
