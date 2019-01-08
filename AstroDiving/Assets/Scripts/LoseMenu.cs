using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour {
    
    public GameObject loseMenuUI;
    public GameObject pauseButton;

    public void Activate()
    {
        Time.timeScale = 0f;
        loseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Replay()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
