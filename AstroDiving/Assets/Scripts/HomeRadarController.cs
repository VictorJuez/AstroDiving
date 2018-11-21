using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeRadarController : MonoBehaviour {
    
    public GameObject home;

    private Camera cam;
    
    void Awake()
    {
        cam = Camera.main;
        gameObject.GetComponent<Renderer>().enabled = false;
    }
    
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void FixedUpdate()
    {
        Renderer homeRenderer = home.GetComponent<Renderer>();

        // If home is visible (inside the field of view of the camera) make the radar invisible
        if (homeRenderer.isVisible)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        // If home is outside the field of view the camera, the radar position is the same as  
        // the intersection point, and the radar is visible
        else
        {
            gameObject.transform.position = CalculateRadarPosition();
            if (gameObject.transform.position != Vector3.zero)
                gameObject.GetComponent<Renderer>().enabled = true;
        }
    }


    /*
        Returns the 2D radar position which is the intersection between two line segments
    */
    private Vector2 CalculateRadarPosition()
    {
        // First line segment is composed by the home position and the camera position
        Vector3 homePos = home.transform.position;
        Vector3 cameraPos = cam.transform.position;

        // Second line segment is composed by two of the corners of the camera in world coordinates
        Vector2 cornerBL = cam.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 cornerTL = cam.ScreenToWorldPoint(new Vector2(0, cam.pixelHeight));
        Vector2 cornerTR = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight));
        Vector2 cornerBR = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, 0));

        // If there is an intersection between the line segments, then home is outside the field
        // of view of the camera and the radar must be displayed at the intersection point
        Vector2 intersection = new Vector2();
        if (SegmentsIntersect(homePos, cameraPos, cornerBL, cornerTL))
        {
            intersection = CalculateIntersectionPoint(homePos, cameraPos, cornerBL, cornerTL);
        }
        else if (SegmentsIntersect(homePos, cameraPos, cornerTL, cornerTR))
        {
            intersection = CalculateIntersectionPoint(homePos, cameraPos, cornerTL, cornerTR);
        }
        else if (SegmentsIntersect(homePos, cameraPos, cornerTR, cornerBR))
        {
            intersection = CalculateIntersectionPoint(homePos, cameraPos, cornerTR, cornerBR);
        }
        else if (SegmentsIntersect(homePos, cameraPos, cornerBR, cornerBL))
        {
            intersection = CalculateIntersectionPoint(homePos, cameraPos, cornerBR, cornerBL);
        }
       
        return intersection;
    }


    /*
        Calculates wheter two 2D line segments intersect or not in the 2D space
    */
    private bool SegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        var d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);

        if (d == 0.0f)
            return false;

        var u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;
        var v = ((p3.x - p1.x) * (p2.y - p1.y) - (p3.y - p1.y) * (p2.x - p1.x)) / d;

        if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
            return false;
        
        return true;
    }


    /*
        Calculates and returns the intersection point of two 2D line segments
    */
    private Vector2 CalculateIntersectionPoint(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        var d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);
        
        var u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;

        Vector2 intersection = new Vector2
        {
            x = p1.x + u * (p2.x - p1.x),
            y = p1.y + u * (p2.y - p1.y)
        };

        return intersection;
    }
    
}
