using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleAttractor : MonoBehaviour {

    private float speed = 10.0f;

    // Total distance between the markers.
    private List<AttractedObject> collidedObjects = new List<AttractedObject>();

    private Transform blackHoleTransform;

    // Use this for initialization
    void Start ()
    {
        this.blackHoleTransform = GetComponent<Transform>();
    }

    /*
     * Want to keep checking for objects in the range
     * 
     */

    public void Update()
    {
        // Distance moved equals elapsed time times speed..

        foreach (AttractedObject obj in collidedObjects)
        {
            float distCovered = (Time.time - obj.time) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / obj.journeyLength;

            // Set our position as a fraction of the distance between the markers.
            obj.satellites.transform.position = Vector3.Lerp(obj.initialPos, blackHoleTransform.position, fractionOfJourney);
        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SHIP IS HERE");
        //Ship shipComponent = other.gameObject.GetComponent<Ship>();
        //if (shipComponent != null)
        if (other.gameObject.GetComponent<Ship>() != null)
        {
            Debug.Log("SHIP IS HERE");
            // Change the cube color to green.
            this.collidedObjects.Add(new AttractedObject(Time.time, Vector3.Distance(other.transform.position, blackHoleTransform.position), other.transform.position, other.gameObject));
        }
    }

}

class AttractedObject
{
    public float time;
    public float journeyLength;
    public Vector3 initialPos;
    public GameObject satellites;

    public AttractedObject(float time,
                    float journeyLength,
                    Vector3 initialPos,
                    GameObject satellites)
    {
        this.time = time;
        this.initialPos = initialPos;
        this.journeyLength = journeyLength;
        this.satellites = satellites;
    }
}
