using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField]
    private MainMenu _mainMenu;

    [Header("Menu Buttons")] 
    [SerializeField]
    private Button backButton;
    
    private SaveSlot[] _saveSlots;
    private bool _isLoadingGame = false;

    private void Awake()
    {
        _saveSlots = GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        // Disable all buttons to prevent extra clicking on buttons. Used as a precaution
        DisableMenuButtons();
        
        Debug.Log("You clicked on save: " + saveSlot.GetProfileId());
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
        
        if (!_isLoadingGame)
            DataPersistenceManager.instance.NewGame();
        
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Level01");
    }
    
    public void BackToMainMenu()
    {
        _mainMenu.ActivateMenu();
        DeactivateMenu();
    }
    public void ActivateMenu(bool isLoadingGame)
    {
        gameObject.SetActive(true);
        _isLoadingGame = isLoadingGame;
        
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach (SaveSlot saveSlot in _saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
            }
        }
    }
    
    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in _saveSlots)
        {
            saveSlot.SetInteractable(false);
        }

        backButton.interactable = false;
    }
}
