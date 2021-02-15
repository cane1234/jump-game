using System;
using UnityEngine;



public class BaseGameController : Singleton<BaseGameController>
{
    #region Editor Fields
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private GameObject floor;

    [SerializeField]
    private GameObject stepPrefab;


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

    #endregion

    #region Properties
    public GameObject Floor
    {
        get { return floor; }
    }
    #endregion

    #region Private Fields

    private GameObject currentHighestStep;

    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        currentHighestStep = FindObjectOfType<StepController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSteps();
    }

    #endregion

    #region Private Methods
    private void UpdateSteps()
    {
        float currentPlayerPos = GetPlayerY();
        float currentHighestStepPos = currentHighestStep.GetComponent<StepController>().GetStepY();

        if(currentHighestStepPos - currentPlayerPos < 20)
        {
            CreateStep(currentHighestStepPos);
        }
    }

    private void CreateStep(float currentHighestStepPos)
    {
        float min_y = currentHighestStepPos + spawnStepsMinY;
        float max_y = currentHighestStepPos + spawnStepsMaxY;

        float x = UnityEngine.Random.Range(spawnStepsMinX, spawnStepsMaxX);
        float y = UnityEngine.Random.Range(min_y, max_y);

        GameObject newStep = Instantiate(stepPrefab, new Vector3(x, y, 0), Quaternion.identity);
        currentHighestStep = newStep;
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

    public void EndGame()
    {
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

    #endregion
}
