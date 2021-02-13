using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresButtonManager : MonoBehaviour
{
    public Button ToMainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        RegisterEvents();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        UnregisterEvents();
    }

    #region Event Handlers
    private void BackButtonPressed()
    {
        LevelManager.Instance.ToMainMenu();
    }
    #endregion

    #region Helper Methods
    private void RegisterEvents()
    {
        ToMainMenuButton.onClick.AddListener(BackButtonPressed);
    }

    private void UnregisterEvents()
    {
        ToMainMenuButton.onClick.RemoveAllListeners();
    }

    #endregion
}
