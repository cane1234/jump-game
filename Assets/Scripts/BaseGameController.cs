using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameController : Singleton<BaseGameController>
{

    #region Editor Fields
    [SerializeField]
    private PlayerController playerController;

    public GameObject Floor;
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Methods
    public void PauseGame()
    {
        Time.timeScale = 0;
        playerController.PlayerInputEnabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playerController.PlayerInputEnabled = true;
    }

    public float GetPlayerY()
    {
        Bounds playerBounds = playerController.gameObject.GetComponent<BoxCollider2D>().bounds;
        float playerBottom = playerBounds.center.y - playerBounds.extents.y;

        return playerBottom;
    }

    #endregion
}
