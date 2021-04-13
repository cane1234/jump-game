using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    #region Inspector Field

    [SerializeField]
    private GameObject textsParent;

    [SerializeField]
    private TextMeshProUGUI textPrefab;

    #endregion

    #region Private Field

    private List<TextMeshProUGUI> CreatedTexts;

    #endregion

    private void Start()
    {
        CreatedTexts = new List<TextMeshProUGUI>();
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
        for (int i = 0; i < CreatedTexts.Count; i++)
        {
            TextMeshProUGUI entry = CreatedTexts[i];
            CreatedTexts.Remove(entry);
            Destroy(entry.gameObject);
        }

        CreatedTexts.Clear();
    }

    private void CreateNewDisplayEntry(HighScoreEntry entry, int i)
    {
        TextMeshProUGUI newEntry = Instantiate(textPrefab); 
        newEntry.SetText(i + ".       " + entry.Nickname + "                        " + entry.Score);
        newEntry.transform.SetParent(textsParent.transform);
        newEntry.fontSize = 15f;
        //newEntry.color = Color.red;
        newEntry.transform.SetAsLastSibling();

        CreatedTexts.Add(newEntry);
    }
}
