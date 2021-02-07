using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    public Button StartGameButton;
    public Button QuitGameButton;

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

    #endregion

    #region Helper Funtions

    private void RegisterEvents()
    {
        StartGameButton.onClick.AddListener(StartGameButtonPressed);
        QuitGameButton.onClick.AddListener(QuitGameButtonPressed);
    }

    private void UnregisterEvents()
    {
        StartGameButton.onClick.RemoveAllListeners();
        QuitGameButton.onClick.RemoveAllListeners();
    }

    #endregion
}