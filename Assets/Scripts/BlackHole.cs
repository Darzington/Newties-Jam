using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour, IHittable
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
        OnDestroy();
        // APPLY SOME COOL ANIMATION OR PARTICLE FX HERE !
    }

    private void OnCollisionEnter(Collision other)
    {
        Ship ship = other.gameObject.GetComponent<Ship>();
        if (ship != null)
        {
            ship.Spaghettify();
            increaseBlackHole();
        }
    }

    void OnDestroy()
    {
        GameEventManager.Instance.OnBlackHoleSizeUp -= increaseBlackHole;
        GameEventManager.Instance.OnBlackHoleDestroy -= destroyBlackHole;
        Destroy(this.gameObject);
    }

    public void Hit()
    {
        destroyBlackHole();
    }
}
