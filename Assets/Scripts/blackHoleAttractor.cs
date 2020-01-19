using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleAttractor : MonoBehaviour {

    public float speed = 10.0f;

    // Total distance between the markers.
    private List<AttractedObject> collidedObjects = new List<AttractedObject>();

    private Transform blackHoleTransform;

    // Mesh deforming
    Mesh deformingMesh;
    Vector3[] originalVertices, displacedVertices;
    Vector3[] vertexVelocities;

    public float force = 10f;

    MeshDeformer deformer;

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

            Ray inputRay = this.blackHoleTransform.position;
            Raycast hit;

            if (Physics.Raycast(inputRay, out hit))
            {
                MeshDeformer deformer = obj.satellites.GetComponentInParent<MeshDeformer>();

                if (deformer)
                {
                    Vector3 point = obj.point;
                    deformer.AddDeformingForce(point, force);
                }
            }

        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Ship>() != null)
        {
            this.deformingMesh = other.gameObject.GetComponentInParent<MeshFilter>().mesh;
            this.originalVertices = this.deformingMesh.vertices;
            this.displacedVertices = new Vector3[this.originalVertices.Length];

            for (int i = 0; i < originalVertices.Length; i++)
            {
                displacedVertices[i] = originalVertices[i];
            }

            this.vertexVelocities = new Vector3[this.originalVertices.Length];

            Debug.Log("SHIP IS HERE");
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
