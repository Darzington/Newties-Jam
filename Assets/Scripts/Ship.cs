﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IHittable
{
    private int shootShipBalanceChange = 4, shipSpaghettifyBalanceChange = 7;

    [SerializeField] GameObject spaghetti, explosion;

    private void Start()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }

    public void Hit()
    {
        DestroyAndChangeBalance(shootShipBalanceChange);
        GameObject splode = Instantiate(explosion, transform.position, Quaternion.identity);
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
