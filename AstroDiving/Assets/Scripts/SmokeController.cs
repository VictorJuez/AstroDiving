using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour {

	private ParticleSystem part;

	void Awake() {
		part = GameObject.Find("WhiteSmoke").GetComponent<ParticleSystem>();
        part.Stop();
		
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enableSmoke(){
		part.Play();
	}

	public void disableSmoke(){
		part.Stop();
	}
}
