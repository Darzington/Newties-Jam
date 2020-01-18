using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    public AK.Wwise.Event music;

    public GameObject wwiseObj;
    // Start is called before the first frame update
    void Start()
    {
        music.Post(wwiseObj);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
