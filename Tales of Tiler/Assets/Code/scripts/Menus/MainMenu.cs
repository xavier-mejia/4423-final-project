using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject _optionsMenu;
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}
