using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IHittable
{
    private int shootShipBalanceChange = 5, shipSpaghettifyBalanceChange = 4;

    public void Hit()
    {
        DestroyAndChangeBalance(shootShipBalanceChange);
    }

    public void Spaghettify()
    {
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
