using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalckHoleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject blackHole;
    [SerializeField]
    Transform[] blackHolesPos;

    List<GameObject> blackHolesList;
    List<int> indexUsed;

    private float timeBetweenSpawns = 3f;

    void Start()
    {
        indexUsed = new List<int>();
        blackHolesList = new List<GameObject>();
        StartCoroutine(SpawnBlackHoles());
        StartCoroutine(TimerCountdown());
    }
    
    private IEnumerator SpawnBlackHoles()
    {
        if (indexUsed.Count == blackHolesPos.Length)
            yield break;
        int index = Random.Range(0, blackHolesPos.Length);
        while(!isValidIndex(index))
        {
            index = Random.Range(0, blackHolesPos.Length);
        }
        GameObject instance = Instantiate(blackHole, new Vector3(blackHolesPos[index].position.x, blackHole.transform.position.y, blackHolesPos[index].position.z), blackHole.transform.rotation);
        instance.transform.SetParent(blackHolesPos[index]);
        yield return new WaitForSeconds(timeBetweenSpawns);
        StartCoroutine(SpawnBlackHoles());
    }

    private bool isValidIndex(int index)
    {
        foreach (var item in indexUsed)
        {
            if (item == index)
                return false;
        }
        indexUsed.Add(index);
        return true;
    }

    //EVERY 10 SECOONDS, THE TIME BETWEEN SPAWNS IS REDUCED BY 1 SECOND, STARTING AT 5
    private IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(10);
        decreaseTime();
        if (timeBetweenSpawns == 1)
            yield break;
        StartCoroutine(TimerCountdown());
    }

    private void decreaseTime()
    {
        timeBetweenSpawns--;
    }

}
