using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlanet : MonoBehaviour
{
    [Range(1f,10f)]
    public float speed ;
    private bool orbit;
    private Vector2 orbitAngle;
    private Vector2 direction = new Vector2(1, 0);
    private Transform orbitPlanet;

    O2Controller O2Controller;

    private void Awake()
    {
        O2Controller = GetComponent<O2Controller>();
    }

    // Use this for initialization
    void Start()
    {
        orbit = false;
    }


    // Update is called once per frame
    void Update()
    {
    }
 
     void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orbit = false;
            O2Controller.SetOrbitingO2Planet(false);
            speed = Mathf.Abs(speed);
            transform.rotation = Quaternion.identity; 
            Debug.Log("<color=blue>SPACE PRESSED : </color>"  + direction + "<color=blue> Speed : </color>" + speed );
        }
        else if(!orbit){
            Debug.Log("<color=white>Translate : </color>" + direction + "<color=white> Speed : </color>" + speed);
            Debug.Log("<color=pink>orbit : </color>" + orbit);
            // transform.Translate(direction * speed * Time.deltaTime);
            transform.Translate(direction * speed * Time.deltaTime);
         }
         else
         {
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
            // Debug.Log("<color=yellow>Rotate Around : </color>" + direction + "<color=yellow> Speed : </color>" + speed);
         }
     }

    void OnCollisionEnter2D(Collision2D other)
    {
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

        if (other.gameObject.CompareTag("OxigenPlanet"))
        {
            Debug.Log("oxigen");
            O2Controller.SetOrbitingO2Planet(true);
        }
    }
}
