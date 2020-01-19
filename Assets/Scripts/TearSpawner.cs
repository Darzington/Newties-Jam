using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TearSpawner : MonoBehaviour
{
    public GameObject basicTear;
    public Sprite tear1, tear2, tear3;
    private float timeBetweenTears = 2.0f, randomTearTime = 0.5f;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(Cry());
    }

    private IEnumerator Cry()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenTears + Random.Range(-randomTearTime, randomTearTime));
            makeTear();
        }
    }

    private void makeTear()
    {
        GameObject tearObject = Instantiate(basicTear, this.transform);
        Image im = tearObject.GetComponent<Image>();
        int tearIndex = Random.Range(0, 3);
        if (tearIndex == 0)
        {
            im.sprite = tear1;
        }
        else if (tearIndex == 1)
        {
            im.sprite = tear2;
        }
        else if (tearIndex == 2)
        {
            im.sprite = tear3;
        }
        StartCoroutine(TearSelfDestruct(tearObject));
    }

    private IEnumerator TearSelfDestruct(GameObject toDestruct)
    {
        float endTime = Time.time + 4.0f;
        Transform cry = toDestruct.transform;
        while (Time.time < endTime)
        {
            cry.Translate(Vector3.down * 8);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(toDestruct);
    }

}
