using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuctions : MonoBehaviour
{
    public void LoadLeaderboardIntro()
    {
        SceneManager.LoadScene("LeaderboardIntro");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
