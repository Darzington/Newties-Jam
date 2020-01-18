﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IHittable
{
    [SerializeField] private int shootShipBalanceChange = 10, shipSpaghettifyBalanceChange = 4;

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
