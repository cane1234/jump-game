using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    #region Level Names

    private const string gameEntryLevelName = "GameEntry";
    private const string mainMenuLevelName = "MainMenu";
    private const string levelOneName = "Level1";
    private const string highScoresLevelName = "HighScores";
    private const string endGameLevelname = "EndGame";

    #endregion

    #region Private Fields

    private string currentLevelName;

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentLevelName = gameEntryLevelName;
        Debug.Log("Level Manager: Started. Current level: " + currentLevelName);
        LoadLevel(mainMenuLevelName);
    }

    #endregion

    #region Private Methods
    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        currentLevelName = levelName;

        Debug.Log("Level Manager: Level loaded: " + currentLevelName);
    }

    #endregion

    #region Public Methods
    public void StartGame()
    {
        LoadLevel(levelOneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HighScores()
    {
        LoadLevel(highScoresLevelName);
    }

    public void ToMainMenu()
    {
        LoadLevel(mainMenuLevelName);
    }

    #endregion
}