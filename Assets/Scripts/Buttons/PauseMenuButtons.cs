#region Usings
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class PauseMenuButtons : MonoBehaviour
{
    #region Editor fields

    [SerializeField]
    private Button ContinueButton;

    [SerializeField]
    private Button MainMenuButton;

    [SerializeField]
    private Button QuitGameButton;

    [SerializeField]
    private GameObject MenuRoot;

    #endregion

    #region Private Fields

    private State currentState;

    #endregion

    #region Enum
    private enum State
    {
        Visible,
        Hidden
    }

    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        HideMenu();
        ResumeGame();
        RegisterEvents();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == State.Hidden)
            {
                ShowMenu();
                PauseGame();
            }
            else
            {
                HideMenu();
                ResumeGame();
            }
        }
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }

    #endregion

    #region Helper Methods

    private void RegisterEvents()
    {
        ContinueButton.onClick.AddListener(ContinueGamePressed);
        MainMenuButton.onClick.AddListener(MainMenuPressed);
        QuitGameButton.onClick.AddListener(QuitGamePressed);
    }

    private void UnregisterEvents()
    {
        ContinueButton.onClick.RemoveAllListeners();
        MainMenuButton.onClick.RemoveAllListeners();
        QuitGameButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region Private Methods

    private void ShowMenu()
    {
        MenuRoot.SetActive(true);
        currentState = State.Visible;
    }

    private void HideMenu()
    {
        MenuRoot.SetActive(false);
        currentState = State.Hidden;
    }

    private void ResumeGame()
    {
        LevelManager.Instance.ResumeGame();
    }

    private void PauseGame()
    {
        LevelManager.Instance.PauseGame();
    }

    #endregion

    #region Event Handlers

    private void ContinueGamePressed()
    {
        ResumeGame();
        HideMenu();
    }

    private void MainMenuPressed()
    {
        HideMenu();
        LevelManager.Instance.ToMainMenu();
    }

    private void QuitGamePressed()
    {
        HideMenu();
        LevelManager.Instance.QuitGame();
    }
    #endregion

}
