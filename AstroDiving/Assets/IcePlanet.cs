using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlanet : MonoBehaviour
{
    [Range(5f,20f)]
    public float speed;
    public LoseMenu loseMenu;
    public WinMenu winMenu;

    private bool orbit;
    private Vector2 orbitAngle;
    private Transform orbitPlanet;

    private Vector3 relativeDistance;

    private Vector2 direction = new Vector2(1, 0).normalized;
    private bool gotHome;
    private float speedAux;
    private Vector3 previousPosition = Vector3.zero;

    private GameObject[] blackHoles;

    O2Controller O2Controller;
    BoostController BoostController;

    private void Awake()
    {
        O2Controller = GetComponent<O2Controller>();
        BoostController = GetComponent<BoostController>();
        gotHome = false;
        speedAux = speed;
    }

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
        BoostController.SetBoostEnabled(false);
        speedBoost();
        previousPosition = transform.position;

        if (O2Controller.O2IsGone())
        {
            loseMenu.Activate();
            return;
        }

        if(!orbit){
            // Input.GetMouseButton(0) also captures touch input
            if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0)) && BoostController.ableToBoost()){
                //This part is the one that redirects the direction when keep space pressed to reach a planet
                //Debug.Log("<color=blue>SPACE PRESSED changing trajectory: </color>"  + direction + "<color=blue> Speed : </color>" + speed );
                BoostController.SetBoostEnabled(true);
                speed *=2;
                direction = BoostController.calculateBoostDirection(direction);
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
         }
         else
         {
             if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
            {
                orbit = false;
                BoostController.setTimeAux(BoostController.getTotalTime());
                O2Controller.SetOrbitingO2Planet(false);
                speed = Mathf.Abs(speed);
                Debug.Log("<color=blue>SPACE PRESSED : </color>"  + direction + "<color=blue> Speed : </color>" + speed );
            }
            else{
                Vector2 tempDirection;
                tempDirection = transform.position - orbitPlanet.transform.position;

                Orbit();

                if (speed > 0) {
                    tempDirection = transform.position - orbitPlanet.transform.position;
                }
                else {
                    tempDirection = orbitPlanet.transform.position - transform.position;
                }
                direction = Vector2.Perpendicular(tempDirection).normalized;
            }
         }

        // Orient the player's image to the direction it is moving
        Vector3 dir = previousPosition - transform.position;
        float rotationAngle = Vector3.SignedAngle(dir.normalized, Vector3.right, Vector3.forward);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotationAngle + 90));
    }

    void Orbit()
     {
        //  Vector3 tempDist = relativeDistance;
        // Keep us at the last known relative position
        transform.position = orbitPlanet.transform.position + relativeDistance;
        transform.RotateAround(orbitPlanet.transform.position, orbitPlanet.transform.forward, (180 * speed * Time.deltaTime /(Mathf.Abs(relativeDistance.magnitude) * Mathf.PI)));
        // Reset relative position after rotate
        relativeDistance = transform.position - orbitPlanet.transform.position;
     }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("HomePlanet"))
        {
            gotHome = true;
            winMenu.Activate();
        }

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
        else if (other.gameObject.CompareTag("Planet") || other.gameObject.CompareTag("OxigenPlanet") || other.gameObject.CompareTag("HomePlanet"))
            {
            Debug.Log("<color=red>PLANET HITED : </color>" + other.gameObject.name);
            orbitPlanet = other.gameObject.transform;
            relativeDistance = transform.position - orbitPlanet.transform.position;

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

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Boundary"))
        {
            //O2Controller.SetOutsideBoundaries(!O2Controller.GetOutsideBoundaries());
            O2Controller.SetOutsideBoundaries(true);
            Debug.Log("OutsideBoundaries:" + O2Controller.GetOutsideBoundaries());
        }
    }

    public void speedBoost(){
        if(speed > speedAux) speed/=(float)1.1;
        else speed = speedAux;
    }

    public bool GotHome()
    {
        return gotHome;
    }
}
