using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLoad : MonoBehaviour {
	
	public string splashScreenSceneName;

	void Start () {
		StartCoroutine(SplashChange());
	}

	IEnumerator SplashChange(){
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(splashScreenSceneName);
	}
}
