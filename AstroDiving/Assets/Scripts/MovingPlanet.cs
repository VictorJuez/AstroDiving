using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlanet : MonoBehaviour {


	public Vector2 posA;
	public Vector2 posB;
	public float duration;
	private bool horizontal;
	private float lerp;
	private float fact = 1;
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (lerp>=1 || lerp<=0){
			fact *= -1;
		}
		
		lerp = lerp + fact * (Time.deltaTime / duration);
		transform.position = Vector2.Lerp(posA, posB,Mathf.SmoothStep(0.0f, 1.0f, lerp));
	}
}
