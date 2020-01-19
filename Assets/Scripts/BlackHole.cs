using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour, IHittable
{
    private Animator animator;
    private int blackHoleSize = 0, shootBlackHoleBalanceChange = -20;
    
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

    private void destroyBlackHole() // CALLED BY THE SHRINK ANIMATION EVENT
    {
        OnDestroy();
        Destroy(this.gameObject);
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
        GameEventManager.Instance.OnBlackHoleSizeUp -= destroyBlackHole;

    }

    public void Hit()
    {
        MafiaMeter meter = FindObjectOfType<MafiaMeter>();
        if (meter != null)
        {
            meter.ChangeBalance(shootBlackHoleBalanceChange);
        }
        switch (blackHoleSize)
        {
            case 0:
                animator.SetTrigger("shrink");
                break;
            case 1:
                animator.SetTrigger("shrink1");
                break;
            case 2:
                animator.SetTrigger("shrink2");
                break;
            default:
                Debug.LogError("ERROR IN THE BLACK HOLE SWITCH CASE");
                break;
        }
        // AT THE END OF THE ANIMATION, destroyBlackHole IS CALLED
    }
}
