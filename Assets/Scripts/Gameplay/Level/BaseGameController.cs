using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameController : Singleton<BaseGameController>
{

    #region Editor Fields
    [Header("Controllers")]

    public PlayerController PlayerController;

    public StepGeneratorController StepGeneratorController;

    public CameraController CameraController;

    public DifficultyController DifficultyController;

    public PlaceTileFeatureController PlaceTileFeatureController;

    public WallGeneratorController WallGeneratorController;

    [SerializeField]
    private GameObject Floor;

    [SerializeField]
    private Text scoreCount;

    #endregion

    #region Properties
    ///<summary>
    ///Falling speed of all objects in the scene. 
    ///</summary>
    public float FallingSpeed
    {
        get { return DifficultyController.FallingSpeed; }
        set { DifficultyController.FallingSpeed = value; }
    }
    #endregion

    #region Private Fields

    private int stepsClimbed;

    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        stepsClimbed = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Pause
    public void PauseGame()
    {
        Time.timeScale = 0;
        PlayerController.PlayerInputEnabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PlayerController.PlayerInputEnabled = true;
    }
    #endregion

    #region Public Methods
    public void EndGame()
    {
        Debug.Log("Steps climbed: " + stepsClimbed + ".");
        LevelManager.Instance.ToEndGame();
    }

    /// <summary>
    /// Used by steps to calculate if the player is above them, so they can turn on their collider.
    /// </summary>
    /// <returns> Returns the y coordinate of the lowest point of the Player gameObject. </returns>
    public float GetPlayerBottomY()
    {
        Bounds playerBounds = PlayerController.gameObject.GetComponent<BoxCollider2D>().bounds;
        float playerBottom = playerBounds.center.y - playerBounds.extents.y;

        return playerBottom;
    }

    public Tuple<float, float> GetPlayerCenterPosition()
    {
        Bounds playerBounds = PlayerController.gameObject.GetComponent<BoxCollider2D>().bounds;
        return new Tuple<float, float>(playerBounds.center.x, playerBounds.center.y);
    }

    public void IncrementScore()
    {
        stepsClimbed++;
        scoreCount.text = stepsClimbed.ToString();
    }

    #endregion
}
