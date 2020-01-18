using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    [HideInInspector] public int score;

    private void Start()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += WorkWithLeaderboard;
    }

    private void WorkWithLeaderboard(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LeaderboardIntro")
        {
            NewLeaderboardEntry nle = FindObjectOfType<NewLeaderboardEntry>();
            nle.score = this.score;
        }
        else if (scene.name == "Leaderboard")
        {
            Destroy(this.gameObject);
        }
    }
}
