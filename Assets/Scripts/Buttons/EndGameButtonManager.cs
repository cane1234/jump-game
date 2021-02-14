using UnityEngine.UI;
using UnityEngine;

public class EndGameButtonManager : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private Button ToMainMenuButton;

    #endregion

    #region Unity Methods
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

    #endregion

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