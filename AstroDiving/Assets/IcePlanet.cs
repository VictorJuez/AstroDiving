using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlanet : MonoBehaviour
{
    [Range(1f,10f)]
    public float speed;

    private bool orbit;
    private Vector2 orbitAngle;
    private Vector2 direction = new Vector2(1, 0);
    private Transform orbitPlanet;

    private GameObject[] blackHoles;

    // Use this for initialization
    void Start()
    {
        orbit = false;

        // Find all the objects with the "BlackHole" tag
        blackHoles = GameObject.FindGameObjectsWithTag("BlackHole");

    }


    // Update is called once per frame
    void Update()
    {
          if(!orbit){
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
         }
         else
         {
             if (Input.GetKeyDown(KeyCode.Space))
            {
                orbit = false;
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
            foreach (GameObject blackHole in blackHoles){
                // Search for the other blackhole and teleport the player
                if(!GameObject.ReferenceEquals(blackHole, other.gameObject)){
                    // Get the CircleCollider radius 
                    float blackHoleColliderRadius = blackHole.transform.GetComponent<CircleCollider2D>().radius;
                    // Move the player to the other blackhole's center + his radius
                    transform.position = blackHole.transform.position + Vector3.ClampMagnitude(direction, blackHoleColliderRadius);
                    break;
                }
            }
        }
        else{
            Debug.Log("<color=red>PLANET ORBITE HITED : </color>" + other.gameObject.name);
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
        }
    }
}
