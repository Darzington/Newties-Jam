using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuctions : MonoBehaviour
{
    public AK.Wwise.Event stopMusic;

    public GameObject wwiseObj;
    public void LoadLeaderboardIntro()
    {
        SceneManager.LoadScene("LeaderboardIntro");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("Intro");
        stopMusic.Post(wwiseObj);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
        stopMusic.Post(wwiseObj);
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
