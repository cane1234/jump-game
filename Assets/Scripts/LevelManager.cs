using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    #region Level Names

    private const string mainMenuLevelName = "MainMenu";
    private const string levelOneName = "Level1";

    #endregion

    #region Private Fields

    private string currentLevelName;

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        currentLevelName = mainMenuLevelName;
        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Level Manager: Started. Current level: " + mainMenuLevelName);
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

    #endregion
}