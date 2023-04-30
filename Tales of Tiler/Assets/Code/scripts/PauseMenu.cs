using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUi;
    [SerializeField] private PlayerController _playerController;
    
    public bool Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true; 
        pauseMenuUi.SetActive(true);
        _playerController.isPaused = true;
        return true;
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
        _playerController.isPaused = false;
    }
    
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
