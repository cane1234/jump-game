using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreProcessor : MonoBehaviour
{
    #region Inspector Fields

    [SerializeField]
    private TextMeshProUGUI message;

    [SerializeField]
    private GameObject inputParent;

    [SerializeField]
    private TextMeshProUGUI nicknameInput;

    [SerializeField]
    private Button confirmButton;

    [SerializeField]
    private HighScoreDisplay highScoreDisplay;

    #endregion

    #region Private Fields

    private int currentScore;
    private HighScoreManager highScoreManager;

    #endregion

    #region Unity methods

    private void Start()
    {
        ProcessCurrentScore();

    }

    private void ProcessCurrentScore()
    {
        highScoreManager = LevelManager.Instance.HighScoreManager;
        currentScore = LevelManager.Instance.CurrentScore;

        if (highScoreManager.IsNewHighScoreEntry(currentScore))
        {
            inputParent.SetActive(true);
            message.SetText("Congratulations, you've made it into leaderboard!");

            confirmButton.onClick.AddListener(onConfirmNickname);
        }
        else
        {
            inputParent.SetActive(false);
            message.SetText("More luck next time!");
        }
    }

    private void onConfirmNickname()
    {
        confirmButton.onClick.RemoveListener(onConfirmNickname);
        string nickname = nicknameInput.text;

        highScoreManager.AddHighScoreEntry(nickname, currentScore);
        highScoreDisplay.Show();
    }
    #endregion
}
