using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    

    private Animator animator;
    private int blackHoleSize = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameEventManager.Instance.OnBlackHoleSizeUp += increaseBlackHole;
        GameEventManager.Instance.OnBlackHoleDestroy += destroyBlackHole;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void increaseBlackHole()
    {
        blackHoleSize++;
        if (blackHoleSize > 2)
            blackHoleSize = 2;
        switch (blackHoleSize)
        {
            case 0:
                animator.SetTrigger("spin");
                break;
            case 1:
                animator.SetTrigger("spin1");
                break;
            case 2:
                animator.SetTrigger("spin2");
                break;
            default:
                Debug.LogError("ERROR IN THE BLACK HOLE SWITCH CASE");
                break;
        }
    }

    private void destroyBlackHole()
    {
        onDestroy();
        // APPLY SOME COOL ANIMATION OR PARTICLE FX HERE !
    }

    void onDestroy()
    {
        Debug.Log("BLACK HOLE DESTROYER CALLED, THIS HERE JUST TO CHECK");
        GameEventManager.Instance.OnBlackHoleSizeUp -= increaseBlackHole;
        GameEventManager.Instance.OnBlackHoleSizeUp -= destroyBlackHole;
    }




}
