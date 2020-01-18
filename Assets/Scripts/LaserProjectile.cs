using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private float shootTime = 0.3f, startTime;
    private Vector3 target, startPos;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        startTime = Time.time;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this != null)
        {
            Vector3 currentPos = this.transform.position;
            this.transform.position = Vector3.Lerp(startPos, target, (Time.time - startTime)/shootTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Interact with collision object


        //Destroy this laser
        Destroy(this.gameObject);
    }
}
