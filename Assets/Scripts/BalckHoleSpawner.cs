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

    private float timeBetweenSpawns = 5f;


    void Start()
    {
        indexUsed = new List<int>();
        blackHolesList = new List<GameObject>();
        StartCoroutine(SpawnBlackHoles());
        GameEventManager.Instance.OnDecreaseSpawnTime += decreaseTime;
    }

    void Update()
    {
            
    }
    

    private IEnumerator SpawnBlackHoles()
    {
       
        int index = Random.Range(0, blackHolesPos.Length);
        while(!isValidIndex(index))
        {
            index = Random.Range(0, blackHolesPos.Length);
        }
        GameObject instance = Instantiate(blackHole, blackHolesPos[index].position, Quaternion.identity);
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

    private void decreaseTime()
    {
        timeBetweenSpawns--;
    }

    void OnDestroy()
    {
        GameEventManager.Instance.OnDecreaseSpawnTime -= decreaseTime;
    }
}
