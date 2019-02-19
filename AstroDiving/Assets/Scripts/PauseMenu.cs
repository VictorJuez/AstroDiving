using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
	
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseButton.SetActive(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseButton.SetActive(false);
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

    public void ZoomInOut() {
		var camera = Camera.main;
		var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
		var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
		if (vcam != null){
            if (vcam.m_Lens.OrthographicSize == 5){
                vcam.m_Lens.OrthographicSize = 20;
            } else if (vcam.m_Lens.OrthographicSize == 20){
                vcam.m_Lens.OrthographicSize = 5;
            }
			
		}
	}
}
