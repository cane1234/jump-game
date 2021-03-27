using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameController : Singleton<BaseGameController>
{
    #region Constants
    private const int wallCreationThreshold = 20;
    #endregion

    #region Editor Fields
    [Header("Controllers")]

    public PlayerController PlayerController;

    public StepGeneratorController StepGeneratorController;

    public CameraController CameraController;

    public DifficultyController DifficultyController;

    public PlaceTileFeatureController PlaceTileFeatureController;

    [SerializeField]
    private GameObject Floor;

    [SerializeField]
    private GameObject WallPrefab;

    [SerializeField]
    private Text scoreCount;



    [Header("Walls")]
    [SerializeField]
    private GameObject currentHighestLeftWall;

    [SerializeField]
    private GameObject currentHighestRightWall;

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

    private GameObject currentHighestStep;

    private WallController currentHighestLeftWallController;

    private int stepsClimbed;

    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {

        currentHighestLeftWallController = currentHighestLeftWall.GetComponent<WallController>();
        
        stepsClimbed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWalls();
    }

    #endregion

    #region Wall Creation
    private void UpdateWalls()
    {
        float currentPlayerPos = GetPlayerY();

        float currentHighestLeftWallPos = currentHighestLeftWallController.GetWallY();
        //float currentHighestRightWallPos = currentHighestRightWall.GetComponent<WallController>().GetWallY();

        if(currentHighestLeftWallPos - currentPlayerPos < wallCreationThreshold)
        {
            CreateWalls(currentHighestLeftWallPos, currentHighestLeftWall.transform.position.x, currentHighestRightWall.transform.position.x);
        }
    }

    private void CreateWalls(float y, float leftX, float rightX)
    {
        GameObject newLeftWall = Instantiate(WallPrefab, new Vector3(leftX, y, 0), Quaternion.identity);
        GameObject newRightWall = Instantiate(WallPrefab, new Vector3(rightX, y, 0), Quaternion.identity);

        currentHighestLeftWallController = newLeftWall.GetComponent<WallController>();

        currentHighestLeftWall = newLeftWall;
        currentHighestRightWall = newRightWall;
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
    public float GetPlayerY()
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
