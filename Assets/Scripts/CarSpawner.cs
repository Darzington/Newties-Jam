﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public enum Direction { Left, Right };

    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private Direction dir;
    [SerializeField] private float moveSpeed, minMoveSpeed = 10.0f, maxMoveSpeed = 30.0f, timeBetweenSpawns, randomSpawnTimeOffset;

    private Vector3 directionVector;

    void Start()
    {
        moveSpeed = 10.0f;
        SetTimeBetweenSpawns(9.0f);
        directionVector = GetDirection();
        StartCoroutine(SpawnCarsForever());
    }

    private IEnumerator SpawnCarsForever()
    {
        SetTimeBetweenSpawns(timeBetweenSpawns - Time.timeSinceLevelLoad * 0.01f);
        RandomizeMoveSpeed();

        GameObject ship = Instantiate(shipPrefab, this.transform);
        Rigidbody rb = ship.GetComponent<Rigidbody>();
        rb.AddForce(directionVector * moveSpeed, ForceMode.VelocityChange);

        yield return new WaitForSeconds(timeBetweenSpawns + Random.Range(-randomSpawnTimeOffset, randomSpawnTimeOffset));

        StartCoroutine(SpawnCarsForever());
    }

    private void RandomizeMoveSpeed()
    {
        this.moveSpeed *= Random.Range(0.9f, 1.5f);
        this.moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed + Random.Range(0.0f, 0.3f), maxMoveSpeed + Random.Range(0.0f, 0.3f));
    }

    private void SetTimeBetweenSpawns(float timeBetweenSpawns)
    {
        this.timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns, 2.0f + Random.Range(0.0f, 0.3f), 10.0f);
        this.randomSpawnTimeOffset = 0.2f * timeBetweenSpawns;
    }

    private Vector3 GetDirection()
    {
        return new Vector3(dir == Direction.Left ? -1 : 1, 0, 0);
    }
}
