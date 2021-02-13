using UnityEngine.UI;
using UnityEngine;

public class EndGameButtonManager : MonoBehaviour
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
    private void ToMainMenuButtonPressed()
    {
        LevelManager.Instance.ToMainMenu();
    }
    #endregion

    #region Helper Methods
    private void RegisterEvents()
    {
        ToMainMenuButton.onClick.AddListener(ToMainMenuButtonPressed);
    }

    private void UnregisterEvents()
    {
        ToMainMenuButton.onClick.RemoveAllListeners();
    }
    #endregion
}
