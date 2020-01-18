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
        TextAsset leaderboardRecord = Resources.Load<TextAsset>("Leaderboard");

        using (FileStream fs = new FileStream("Assets/Resources/Leaderboard.txt", FileMode.Append))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                if (name.text == "")
                {
                    name.text = "No name";
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
