using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonManager : MonoBehaviour {

    public IcePlanet IcePlanet;
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (IcePlanet.GotHome())
        {
            anim.SetTrigger("GameWon");
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("LevelX");
            }
        }
	}
}
