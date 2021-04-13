using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    public void Reload()
    {
        HighScoreManager highScoreManager = LevelManager.Instance.HighScoreManager;
        List<HighScoreEntry> list = highScoreManager.HighScores;
        
        foreach(HighScoreEntry entry in list)
        {
            Debug.Log(entry.Nickname + " " + entry.Score);
        }
    }
}
