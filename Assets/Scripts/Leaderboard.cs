using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
    [SerializeField] private Text leaderboardText;

    void Start()
    {
        UnityEditor.AssetDatabase.ImportAsset("Assets/Resources/Leaderboard.txt");
        TextAsset leaderboardRecord = Resources.Load<TextAsset>("Leaderboard");
        string[] entries = leaderboardRecord.text.Split('\n');

        foreach (string entry in entries)
        {
            entry.Trim();
            if (entry.Length > 0)
            {
                int score = int.Parse(entry.Substring(entry.LastIndexOf(' ')).Trim());
                string name = entry.Substring(0, entry.LastIndexOf(' '));
                leaderboard.Add(new LeaderboardEntry(name, score));
            }
        }

        FillLeaderboardText();
    }

    private void FillLeaderboardText()
    {
        leaderboard.Sort(SortByScore);

        string leaderboardContents = "";
        foreach (LeaderboardEntry entry in leaderboard)
        {
            leaderboardContents += entry.ToString() + "\n";
        }

        leaderboardText.text = leaderboardContents;
    }

    private class LeaderboardEntry
    {
        public string name;
        public int score;

        public LeaderboardEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public override string ToString()
        {
            return name + "  Score: " + score;
        }
    }

    private static int SortByScore(LeaderboardEntry entry1, LeaderboardEntry entry2)
    {
        return entry2.score.CompareTo(entry1.score);
    }
}
