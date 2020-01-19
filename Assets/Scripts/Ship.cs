using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IHittable
{
    private int shootShipBalanceChange = 20, shipSpaghettifyBalanceChange = 30;
    [SerializeField] GameObject spaghetti, explosion;

    public GameObject wwiseObj;
    public AK.Wwise.Event explosionEventBoom;

    private void Start()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);

        wwiseObj = FindObjectOfType<AkInitializer>().gameObject;
    }

    public void Hit()
    {
        DestroyAndChangeBalance(shootShipBalanceChange);
        GameObject splode = Instantiate(explosion, transform.position, Quaternion.identity);

        explosionEventBoom.Post(wwiseObj);

    }

    public void Spaghettify()
    {
        Instantiate(spaghetti, transform.position, Quaternion.identity);

        DestroyAndChangeBalance(shipSpaghettifyBalanceChange);
    }

    private void DestroyAndChangeBalance(int balanceChange)
    {
        MafiaMeter meter = FindObjectOfType<MafiaMeter>();
        if (meter != null)
        {
            meter.ChangeBalance(shootShipBalanceChange);
        }

        Destroy(this.gameObject);
    }
}
