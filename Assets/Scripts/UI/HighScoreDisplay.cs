using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    #region Inspector Field

    [SerializeField]
    private GameObject namesParent;

    [SerializeField]
    private GameObject scoreParent;

    [SerializeField]
    private TextMeshProUGUI textPrefab;

    #endregion

    #region Private Field

    private List<TextMeshProUGUI> CreatedNicknameTexts;
    private List<TextMeshProUGUI> CreatedScoreTexts;

    #endregion

    private void Start()
    {
        CreatedNicknameTexts = new List<TextMeshProUGUI>();
        CreatedScoreTexts = new List<TextMeshProUGUI>();
        Reload();
    }

    public void Reload()
    {
        Clear();

        HighScoreManager highScoreManager = LevelManager.Instance.HighScoreManager;
        List<HighScoreEntry> list = highScoreManager.HighScores;
        list.Sort();

        for (int i = 0; i < list.Count; i++)
        {
            HighScoreEntry entry = list[i];
            CreateNewDisplayEntry(entry, i + 1);
        }
    }

    private void Clear()
    {
        foreach(TextMeshProUGUI obj in CreatedNicknameTexts)
        {
            Destroy(obj.gameObject);
        }

        foreach (TextMeshProUGUI obj in CreatedScoreTexts)
        {
            Destroy(obj.gameObject);
        }

        CreatedNicknameTexts.Clear();
        CreatedScoreTexts.Clear();
    }

    private void CreateNewDisplayEntry(HighScoreEntry entry, int i)
    {
        CreateNameText(entry, i);
        CreateScoreTexts(entry, i);
    }

    private void CreateNameText(HighScoreEntry entry, int i)
    {
        TextMeshProUGUI newEntry = Instantiate(textPrefab);
        newEntry.SetText(i + ".       " + entry.Nickname);
        newEntry.transform.SetParent(namesParent.transform);
        //newEntry.fontSize = 15f;
        newEntry.color = i % 2 == 0 ? new Color(1f,0.5f,0f) : Color.yellow;
        newEntry.transform.localScale = new Vector3(1, 1, 1);
        newEntry.transform.SetAsLastSibling();
        CreatedNicknameTexts.Add(newEntry);
    }

    private void CreateScoreTexts(HighScoreEntry entry, int i)
    {
        TextMeshProUGUI newEntry = Instantiate(textPrefab);
        newEntry.SetText(entry.Score.ToString());
        newEntry.transform.SetParent(scoreParent.transform);
        //newEntry.fontSize = 15f;
        newEntry.color = i % 2 == 0 ? new Color(1f, 0.5f, 0f) : Color.yellow;
        newEntry.transform.localScale = new Vector3(1, 1, 1);
        newEntry.transform.SetAsLastSibling();
        CreatedScoreTexts.Add(newEntry);
    }
}
