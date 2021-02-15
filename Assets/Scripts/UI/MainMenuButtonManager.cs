using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private Button StartGameButton;

    [SerializeField]
    private Button QuitGameButton;

    [SerializeField]
    private Button HighScoresButton;

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }
    #endregion

    #region Event Handlers
    private void StartGameButtonPressed()
    {
        LevelManager.Instance.StartGame();
    }

    private void QuitGameButtonPressed()
    {
        LevelManager.Instance.QuitGame();
    }

    private void HighScoresButtonPressed()
    {
        LevelManager.Instance.HighScores();
    }

    #endregion

    #region Helper Funtions

    private void RegisterEvents()
    {
        StartGameButton.onClick.AddListener(StartGameButtonPressed);
        QuitGameButton.onClick.AddListener(QuitGameButtonPressed);
        HighScoresButton.onClick.AddListener(HighScoresButtonPressed);
    }

    private void UnregisterEvents()
    {
        StartGameButton.onClick.RemoveAllListeners();
        QuitGameButton.onClick.RemoveAllListeners();
        HighScoresButton.onClick.RemoveAllListeners();
    }

    #endregion
}