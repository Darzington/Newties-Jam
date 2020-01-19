using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3.0f);

        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
