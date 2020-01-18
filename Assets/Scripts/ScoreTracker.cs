using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private const string scoreTextBeginning = "Score: ";

    [SerializeField] private int scoreGainPerDeltaTime = 1;
    private int score = 0;

    void Update()
    {
        score += scoreGainPerDeltaTime;
        scoreText.text = scoreTextBeginning + score;
    }

    public void ChangeScore(int scoreToAddOrSubtract)
    {
        score += scoreToAddOrSubtract;
        score = Mathf.Min(0, score);
    }

    public int GetScore()
    {
        return score;
    }
}
