using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlanet : MonoBehaviour
{
    [Range(5f,20f)]
    public float speed;

    private bool orbit;
    private Vector2 orbitAngle;
    private Vector2 direction = new Vector2(1, 0).normalized;
    private Transform orbitPlanet;

    private GameObject[] blackHoles;
    private GameObject[] Planets;

    O2Controller O2Controller;

    private void Awake()
    {
        O2Controller = GetComponent<O2Controller>();
    }

    // Use this for initialization
    void Start()
    {
        orbit = false;

        // Find all the objects with the "BlackHole" tag
        blackHoles = GameObject.FindGameObjectsWithTag("BlackHole");
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        orbitPlanet = GetNearestPlanet();
    }

    private Transform GetNearestPlanet()
    {
        float minDistance = Vector2.Distance(transform.position, Planets[0].transform.position);
        GameObject nearestPlanet = Planets[0];

        for (int i = 1; i < Planets.Length; ++i){
            float auxDistance = Vector2.Distance(transform.position, Planets[i].transform.position);
            if (auxDistance < minDistance) {
                minDistance = auxDistance;
                nearestPlanet = Planets[i];
            }
        }

        return nearestPlanet.transform;
    }


    // Update is called once per frame
    void Update()
    {


        if(!orbit){
            // Input.GetMouseButton(0) also captures touch input
            if (Input.GetKey(KeyCode.Space)|| Input.GetMouseButtonDown(0)){
                //This part is the one that redirects the direction when keep space pressed to reach a planet
                Debug.Log("<color=blue>SPACE PRESSED changing trajectory: </color>"  + direction + "<color=blue> Speed : </color>" + speed );
                orbitPlanet = GetNearestPlanet();
                Vector2 tmpDirection;
                tmpDirection = transform.position - orbitPlanet.transform.position;
                tmpDirection *= -1;
                tmpDirection = (direction + tmpDirection).normalized;
                tmpDirection = (direction + tmpDirection).normalized;
                tmpDirection = (direction + tmpDirection).normalized;
                tmpDirection = (direction + tmpDirection).normalized;

                direction = (direction + tmpDirection).normalized;

                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else transform.Translate(direction * speed * Time.deltaTime, Space.World);
         }
         else
         {
             if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
            {
                orbit = false;
                O2Controller.SetOrbitingO2Planet(false);
                speed = Mathf.Abs(speed);
                Debug.Log("<color=blue>SPACE PRESSED : </color>"  + direction + "<color=blue> Speed : </color>" + speed );
            }
            else{
                Vector2 tempDirection;
                tempDirection = transform.position - orbitPlanet.transform.position;

                transform.RotateAround(orbitPlanet.transform.position, orbitPlanet.transform.forward, (180 * speed * Time.deltaTime /(Mathf.Abs(tempDirection.magnitude) * Mathf.PI)));

                if (speed > 0) {
                    tempDirection = transform.position - orbitPlanet.transform.position;
                }
                else {
                    tempDirection = orbitPlanet.transform.position - transform.position;
                }
                direction = Vector2.Perpendicular(tempDirection).normalized;
            }
         }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BlackHole"))
        {
            Debug.Log("<color=red>BLACK HOLE HITED : </color>" + other.gameObject.name);
            foreach (GameObject blackHole in blackHoles){
                // Search for the other blackhole and teleport the player
                if(!GameObject.ReferenceEquals(blackHole, other.gameObject)){
                    // Get the CircleCollider radius 
                    float blackHoleColliderRadius = blackHole.transform.GetComponent<CircleCollider2D>().radius;
                    // Move the player to the other blackhole's center + his radius (works with object scale == 1)
                    Vector3 offsetDirection = direction * blackHoleColliderRadius;
                    transform.position = blackHole.transform.position + offsetDirection;
                    break;
                }
            }
        }
        else if (other.gameObject.CompareTag("Planet") || other.gameObject.CompareTag("OxigenPlanet"))
            {
            Debug.Log("<color=red>PLANET HITED : </color>" + other.gameObject.name);
            orbitPlanet = other.gameObject.transform;
            orbitAngle = other.contacts[0].normal;
            float collisionAngle = Vector2.SignedAngle(direction, orbitAngle);
            
            if(collisionAngle > 0){
                    speed = Mathf.Abs(speed)*(-1);
            }
            else{
                    speed = Mathf.Abs(speed);
            }
            orbit = true;

            if(other.gameObject.CompareTag("OxigenPlanet"))
            {
                Debug.Log("oxigen");
                O2Controller.SetOrbitingO2Planet(true);
            }
        }
        else if (other.gameObject.CompareTag("Asteroid"))
            {
            Debug.Log("<color=red>ASTEROID HITED : </color>" + other.gameObject.name);
                
            orbitAngle = other.contacts[0].normal;
            direction = Vector2.Reflect(direction, orbitAngle);

        }
    }
}
