using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuctions : MonoBehaviour
{
    public AK.Wwise.Event stopMusic;
    public AK.Wwise.Event startButton;
    public AK.Wwise.Event exitButton;

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
        startButton.Post(wwiseObj);
        stopMusic.Post(wwiseObj);
        SceneManager.LoadScene("Intro");
    }

    public void LoadLevel()
    {
        stopMusic.Post(wwiseObj);
        SceneManager.LoadScene("Level");
    }

    public void LoadMenu()
    {
        stopMusic.Post(wwiseObj);
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        exitButton.Post(wwiseObj);
        Application.Quit();
    }

    public void ButtonNoise()
    {
        startButton.Post(wwiseObj);
    }
}
