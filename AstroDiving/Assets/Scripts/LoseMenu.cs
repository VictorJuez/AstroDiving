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
    }

    public void Replay()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
