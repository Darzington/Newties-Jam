using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NewLeaderboardEntry : MonoBehaviour
{
    public Text name;
    public int score = 0;

    public void AddEntry()
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "//Leaderboard.txt", FileMode.Append))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                if (name.text == "")
                {
                    name.text = "No name";
                }

                ScoreKeeper sk = FindObjectOfType<ScoreKeeper>();
                if (sk != null)
                {
                    sk.name = name.text.Trim();
                }

                writer.Write("\n" + name.text.Trim() + " " + score);
                writer.Close();
                writer.Dispose();
            }
            fs.Close();
            fs.Dispose();
        }
    }
}
