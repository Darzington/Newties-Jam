using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour, IHittable
{
    private Animator animator;
    private int blackHoleSize = 0, shootBlackHoleBalanceChange = -20;

    public GameObject wwiseObj;
    public AK.Wwise.Event create;
    public AK.Wwise.Event destroy;
    public AK.Wwise.Event spaghettify;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameEventManager.Instance.OnBlackHoleSizeUp += increaseBlackHole;
        GameEventManager.Instance.OnBlackHoleDestroy += destroyBlackHole;

        wwiseObj = FindObjectOfType<AkInitializer>().gameObject;
        create.Post(wwiseObj);
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
        destroy.Post(wwiseObj);
        OnDestroy();
        MafiaMeter meter = FindObjectOfType<MafiaMeter>();
        if (meter != null)
        {
            meter.ChangeBalance(shootBlackHoleBalanceChange);
        }
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
            spaghettify.Post(wwiseObj);
        }
    }

    void OnDestroy()
    {
        GameEventManager.Instance.OnBlackHoleSizeUp -= increaseBlackHole;
        GameEventManager.Instance.OnBlackHoleSizeUp -= destroyBlackHole;

    }

    public void Hit()
    {
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
        //destroyBlackHole();
    }
}
