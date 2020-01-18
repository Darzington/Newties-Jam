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
            closestHole = blackHoles[0];

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
        foreach (Rigidbody rb in spagRBs)
        {
            rb.AddForce(new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), Random.Range(-15, 15)), ForceMode.VelocityChange);
        }
    }

    private void Update()
    {

    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(4.0f);

        List<MeshRenderer> meshes = new List<MeshRenderer>();
        foreach (Rigidbody rb in spagRBs)
        {
            meshes.Add(rb.gameObject.GetComponent<MeshRenderer>());
        }

        if (closestHole != null)
        {
            foreach (Rigidbody rb in spagRBs)
            {
                rb.AddForce((closestHole.transform.position - rb.transform.position) * 4, ForceMode.Acceleration);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        float startTime = Time.time, fadeTime = 3.0f;
        float progress = 1.0f;
        while (progress > 0.0f)
        {
            foreach (MeshRenderer mesh in meshes)
            {
                progress = Mathf.Lerp(1, 0, (Time.time - startTime) / fadeTime);
                Color currentColor = mesh.material.color;
                mesh.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, progress);
                Debug.Log(progress);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(this.gameObject);
    }
}
