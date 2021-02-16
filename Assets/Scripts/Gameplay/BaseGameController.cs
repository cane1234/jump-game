using UnityEngine;
using UnityEngine.UI;

public class BaseGameController : Singleton<BaseGameController>
{
    #region Constants
    private const int wallCreationThreshold = 20;
    private const int stepCreationThreshold = 20;
    #endregion

    #region Editor Fields
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private GameObject floor;

    [SerializeField]
    private GameObject stepPrefab;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private Text scoreCount;

    [Space(10)]
    [Header("Walls")]
    [SerializeField]
    private GameObject currentHighestLeftWall;

    [SerializeField]
    private GameObject currentHighestRightWall;


    [Space(10)]
    [Header("Step spawn values")]

    [SerializeField]
    private float spawnStepsMinX;

    [SerializeField]
    private float spawnStepsMaxX;

    [SerializeField]
    private float spawnStepsMinY;

    [SerializeField]
    private float spawnStepsMaxY;

    [Space(10)]
    [Header("Difficulty settings")]
    [SerializeField]
    private float fallingSpeed;

    #endregion

    #region Properties
    ///<summary>
    ///Falling speed of all objects in the scene. 
    ///</summary>
    public float FallingSpeed
    {
        get { return fallingSpeed; }
        set { fallingSpeed = value; }
    }
    #endregion

    #region Private Fields

    private GameObject currentHighestStep;
    private StepController currentHighestStepController;

    private WallController currentHighestLeftWallController;

    private int stepsClimbed;

    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        currentHighestStepController = FindObjectOfType<StepController>();
        currentHighestStep = currentHighestStepController.gameObject;

        currentHighestLeftWallController = currentHighestLeftWall.GetComponent<WallController>();
        
        stepsClimbed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSteps();
        UpdateWalls();
    }

    #endregion

    #region Step Creation
    private void UpdateSteps()
    {
        float currentPlayerPos = GetPlayerY();
        float currentHighestStepPos = currentHighestStepController.GetStepY();

        if(currentHighestStepPos - currentPlayerPos < stepCreationThreshold)
        {
            CreateStep(currentHighestStepPos);
        }
    }

    private void CreateStep(float currentHighestStepPos)
    {
        float min_y = currentHighestStepPos + spawnStepsMinY;
        float max_y = currentHighestStepPos + spawnStepsMaxY;

        float x = Random.Range(spawnStepsMinX, spawnStepsMaxX);
        float y = Random.Range(min_y, max_y);

        GameObject newStep = Instantiate(stepPrefab, new Vector3(x, y, 0), Quaternion.identity);
        currentHighestStep = newStep;
        currentHighestStepController = currentHighestStep.GetComponent<StepController>();
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
        GameObject newLeftWall = Instantiate(wallPrefab, new Vector3(leftX, y, 0), Quaternion.identity);
        GameObject newRightWall = Instantiate(wallPrefab, new Vector3(rightX, y, 0), Quaternion.identity);

        currentHighestLeftWallController = newLeftWall.GetComponent<WallController>();

        currentHighestLeftWall = newLeftWall;
        currentHighestRightWall = newRightWall;
    }

    #endregion

    #region Pause
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
        Bounds playerBounds = playerController.gameObject.GetComponent<BoxCollider2D>().bounds;
        float playerBottom = playerBounds.center.y - playerBounds.extents.y;

        return playerBottom;
    }

    public void IncrementScore()
    {
        stepsClimbed++;
        scoreCount.text = stepsClimbed.ToString();
    }

    #endregion
}
