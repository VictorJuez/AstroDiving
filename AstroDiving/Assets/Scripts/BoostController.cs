using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour {
	private float timeAux;
    public float totalTime = 0;
    private const float timeMin = 0.5f;
	private Transform nearestPlanet;
	private O2Controller O2Controller;

	private bool boostEnabled;

	private GameObject[] Planets, Planet, HomePlanet, OxygenPlanet;

	void Awake(){
		timeAux = 0;
		totalTime = 0;
		Planet = GameObject.FindGameObjectsWithTag("Planet");
		HomePlanet = GameObject.FindGameObjectsWithTag("HomePlanet");
		OxygenPlanet = GameObject.FindGameObjectsWithTag("OxigenPlanet");
		
		var list = new List<GameObject>();
        list.AddRange(Planet);
        list.AddRange(HomePlanet);
		list.AddRange(OxygenPlanet);

        // ::: Call ToArray to convert List to array
        Planets = list.ToArray();
		
		nearestPlanet = GetNearestPlanet();
		boostEnabled = false;
		O2Controller = GetComponent<O2Controller>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;
		if (boostEnabled) O2Controller.currentO2 -= 20 * Time.deltaTime;

	}

	public void setTimeAux(float timeAux){
		this.timeAux = timeAux;
	}

	public float getTotalTime(){
		return this.totalTime;
	}

	private Transform GetNearestPlanet()
    {
        float minDistance = Vector2.Distance(transform.position, Planets[0].transform.position);
        GameObject nearestPlanet = Planets[0];

        for (int i = 1; i < Planets.Length; ++i){
            float auxDistance = Vector2.Distance(transform.position, Planets[i].transform.position);
            if (auxDistance < minDistance && sameDirection(Planets[i].transform.position)) {
                minDistance = auxDistance;
                nearestPlanet = Planets[i];
            }
        }

        return nearestPlanet.transform;
    }

	private bool sameDirection(Vector2 planetPosition){

        return true;
    }

	public Vector2 calculateBoostDirection(Vector2 direction){
		nearestPlanet = GetNearestPlanet();
		Vector2 tmpDirection;
		tmpDirection = transform.position - nearestPlanet.transform.position;
		tmpDirection *= -1;
		//float angle = Vector2.Angle(direction, tmpDirection);
		tmpDirection = (direction + tmpDirection).normalized;

		//Debug.Log("<color=green>ANGLE: </color>"  + angle);
		direction = (direction + tmpDirection).normalized;

		return direction;
	}

	public void SetBoostEnabled(bool boostEnabled)
    {
        this.boostEnabled = boostEnabled;
    }

	public bool ableToBoost(){
		if(totalTime > (timeAux+timeMin) && O2Controller.currentO2 > O2Controller.startingO2/4) return true;
		return false;
	}


}
