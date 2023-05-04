using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField]
    private SaveSlotsMenu _saveSlotsMenu;
    
    [SerializeField] private GameObject _optionsMenu;
    
    public void NewGame()
    {
        _saveSlotsMenu.ActivateMenu(false);
        DeactivateMenu();
    }

    public void LoadGame()
    {
        _saveSlotsMenu.ActivateMenu(true);
        DeactivateMenu();

    }
    
    public void OpenOptions()
    {
        _optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        _optionsMenu.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateMenu()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }
}
