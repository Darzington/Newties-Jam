using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    private List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
    [SerializeField] private GameObject leaderboardEntryPrefab;

    void OnEnable()
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
        ScoreKeeper sk = FindObjectOfType<ScoreKeeper>();

        leaderboard.Sort(SortByScore);

        string leaderboardContents = "";
        int i = 0;
        foreach (LeaderboardEntry entry in leaderboard)
        {
            ++i;
            bool isNew = false;
            if (sk != null)
            {
                isNew = entry.name.Equals(sk.name) && entry.score == sk.score;
            }
            FillLeaderboardEntry(entry, i, isNew);
        }
    }

    private void FillLeaderboardEntry(LeaderboardEntry entry, int ranking, bool isNew)
    {
        GameObject entryObject = Instantiate(leaderboardEntryPrefab, this.transform);
        Text number = entryObject.transform.GetChild(0).GetComponent<Text>();
        Text name = entryObject.transform.GetChild(1).GetComponent<Text>();
        Text colon = entryObject.transform.GetChild(2).GetComponent<Text>();
        Text score = entryObject.transform.GetChild(3).GetComponent<Text>();

        number.text = ranking + ".";
        name.text = entry.name;
        score.text = entry.score + "";

        if (isNew)
        {
            number.fontStyle = FontStyle.BoldAndItalic;
            colon.fontStyle = FontStyle.BoldAndItalic;
            name.fontStyle = FontStyle.BoldAndItalic;
            score.fontStyle = FontStyle.BoldAndItalic;

            number.fontSize = number.fontSize + 10;
            colon.fontSize = colon.fontSize + 10;
            name.fontSize = name.fontSize + 10;
            score.fontSize = score.fontSize + 10;
        }
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
    }

    private static int SortByScore(LeaderboardEntry entry1, LeaderboardEntry entry2)
    {
        return entry2.score.CompareTo(entry1.score);
    }
}
