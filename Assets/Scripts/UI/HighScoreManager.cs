using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScoreManager
{
    #region Private Fields
    private List<HighScoreEntry> highScoreList;

    private int maxEntries;

    #endregion

    #region Constructor

    public HighScoreManager()
    {
        maxEntries = 5;
        highScoreList = new List<HighScoreEntry>();
    }

    #endregion

    #region Public methods
    public void SortHighScores()
    {
        highScoreList.Sort();
    }

    public bool IsNewHighScoreEntry(int score)
    {
        if (highScoreList.Count < maxEntries)
            return true;

        return score > GetLowestEntry();
    }

    public int GetLowestEntry()
    {
        SortHighScores();

        return highScoreList.Count > 0 ? highScoreList[highScoreList.Count - 1].Score : 0;
    }

    public void AddHighScoreEntry(string name, int score)
    {
        HighScoreEntry newEntry = new HighScoreEntry(name, score);
        highScoreList.Add(newEntry);
    }

    public List<HighScoreEntry> HighScores { get { return highScoreList; } }

    #endregion

}
