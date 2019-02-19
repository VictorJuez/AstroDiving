using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {
    
    public GameObject winMenuUI;
    public GameObject pauseButton;

    public void Activate()
    {
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
        pauseButton.SetActive(false);
    }
    
    public void NextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex+1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
