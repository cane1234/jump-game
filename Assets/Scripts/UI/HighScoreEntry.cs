using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreEntry : IComparable<HighScoreEntry>
{
    #region Private Fields

    private string nickname;

    private int score;

    #endregion

    #region Public Fields

    public int Score { get { return score; } }

    public string Nickname { get { return nickname; } }
    #endregion

    #region Constructor

    public HighScoreEntry(string _n, int _s)
    {
        nickname = _n;
        score = _s;
    }

    #endregion

    #region IComparer

    public int CompareTo(HighScoreEntry other)
    {
        return other.Score - Score;
    }

    #endregion

}
