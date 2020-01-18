using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private float shootTime = 0.2f, startTime;
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
            float progress = (Time.time - startTime) / shootTime;
            this.transform.position = Vector3.Lerp(startPos, target, progress);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Interact with collision object
        GameObject other = collision.gameObject;
        IHittable hittableComponent = other.GetComponent<IHittable>();
        if (hittableComponent != null)
        {
            hittableComponent.Hit();
        }

        //Destroy this laser
        Destroy(this.gameObject);
    }
}
