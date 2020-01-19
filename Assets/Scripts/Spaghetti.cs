using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaghetti : MonoBehaviour
{
    private Rigidbody[] spagRBs;
    private GameObject closestHole = null;

    void Start()
    {
        spagRBs = GetComponentsInChildren<Rigidbody>();
        FindNearestBlackHole();
        LaunchSpaghetti();
        StartCoroutine(SelfDestruct());
    }

    private void FindNearestBlackHole()
    {
        Vector3 thisPosition = transform.position;
        GameObject[] blackHoles = GameObject.FindGameObjectsWithTag("BlackHole");
        if (blackHoles.Length > 0)
        {
            Vector3 closestObject = Vector3.negativeInfinity;
            for (int i = 0; i < blackHoles.Length; i++)
            {
                Vector3 distanceFromPlayer = (blackHoles[i].transform.position - thisPosition);
                if (distanceFromPlayer.magnitude < closestObject.magnitude)
                {
                    closestObject = distanceFromPlayer;
                    closestHole = blackHoles[i];
                }
            }
        }
    }

    private void LaunchSpaghetti()
    {
        int launchStrength = 10;
        foreach (Rigidbody rb in spagRBs)
        {
            rb.AddForce(new Vector3(Random.Range(-launchStrength, launchStrength), 
                Random.Range(-launchStrength, launchStrength), Random.Range(-launchStrength, launchStrength)), ForceMode.VelocityChange);
        }
    }

    private void Update()
    {

    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.1f);
        float startTime = Time.time, floatTime = 4.0f;
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            progress = (Time.time - startTime) / floatTime;
            if (closestHole != null)
            {
                foreach (Rigidbody rb in spagRBs)
                {
                    rb.AddForce((closestHole.transform.position - rb.transform.position) * progress * 5, ForceMode.Acceleration);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
            {
                progress = 1.0f;
            }
        }
        Debug.Log("DESTROYING THE SPAG");
        Destroy(this.gameObject);
    }
}
