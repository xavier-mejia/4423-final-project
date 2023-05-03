using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;
    
    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }

        instance = this;
    }
    
    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        _dataPersistenceObjects = FindAllDataPersistences();

        LoadGame();
    }
    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO: load any saved data from a file using the data handler
        _gameData = _dataHandler.Load();
        
        //if no data can be loaded, initialize as a new game
        if (_gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        //TODO: push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
        {
            dataPersistence.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        //TODO: pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref _gameData);
        }
        
        //TODO: save that data to a file using the data handler
        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistences()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }
}
