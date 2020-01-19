﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private ScoreKeeper sk;
    private const string scoreTextBeginning = "Score: ";

    [SerializeField] private int scoreGainPerDeltaTime = 1;
    private int score = 0;

    private void Start()
    {
        sk = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        if (Time.timeScale > 0.5f)
        {
            score += scoreGainPerDeltaTime;
            scoreText.text = scoreTextBeginning + score;
        }
    }

    public void ChangeScore(int scoreToAddOrSubtract)
    {
        score += scoreToAddOrSubtract;
        score = Mathf.Min(0, score);
    }

    private void OnDisable()
    {
        if (sk != null)
        {
            sk.score = this.score;
        }
    }
}
