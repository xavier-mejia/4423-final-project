using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUi;
    [SerializeField] private PlayerController playerController;
    
    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true; 
        pauseMenuUi.SetActive(true);
        playerController.isPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");
    }
    public void Resume()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUi.SetActive(false);
        playerController.isPaused = false;
    }
    
    public void QuitToMainMenu()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
