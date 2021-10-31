using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    [SerializeField]
    private TextMeshProUGUI congratulationsText;

    [SerializeField]
    private bool HighScoreScene;
    #endregion

    #region Private Fields

    private int currentScore;
    private HighScoreManager highScoreManager;

    private string savePath;

    #endregion

    #region Unity methods

    private void Start()
    {
        savePath = Application.dataPath + "/mySave.data";

        ProcessCurrentScore();
        if (HighScoreScene)
        {
            highScoreDisplay.Reload();
        }
        congratulationsText.gameObject.SetActive(false);
    }

    private void ProcessCurrentScore()
    {
        ReadHighScoreData();
        //highScoreManager = LevelManager.Instance.HighScoreManager;
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
        inputParent.SetActive(false);

        congratulationsText.SetText("Congratulations " + nickname + "!");
        congratulationsText.gameObject.SetActive(true);

        if (highScoreManager.HighScoreListFull())
        {
            highScoreManager.RemoveLowestEntry();
        }
        highScoreManager.AddHighScoreEntry(nickname, currentScore);

        WriteHighScoreData();
        LevelManager.Instance.HighScoreManager = highScoreManager;
        highScoreDisplay.Reload();
    }

    private void ReadHighScoreData()
    {
        if (File.Exists(savePath))
        {
            FileStream stream = new FileStream(savePath, FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();

            if (stream.Length != 0)
            {
                highScoreManager = binary.Deserialize(stream) as HighScoreManager;
            }
            else
            {
                highScoreManager = new HighScoreManager();
            }
            stream.Close();
        }
        else
        {
            Debug.LogWarning("<color=yellow>Potential error. File on path doesn't exist: " + savePath + " </color>");
            highScoreManager = new HighScoreManager();
            FileStream stream = new FileStream(savePath, FileMode.Create);
            BinaryFormatter binary = new BinaryFormatter();

            binary.Serialize(stream, highScoreManager);
            stream.Close();
        }

        LevelManager.Instance.HighScoreManager = highScoreManager;
    }

    private void WriteHighScoreData()
    {
        FileStream stream = new FileStream(savePath, FileMode.Create);
        BinaryFormatter binary = new BinaryFormatter();
        binary.Serialize(stream, highScoreManager);

        stream.Close();
    }
    #endregion
}
