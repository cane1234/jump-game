using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepGeneratorController : MonoBehaviour
{
    #region Constants

    private const int stepCreationThreshold = 20;
    #endregion

    #region Unity fields

    [SerializeField]
    private GameObject StepRoot;


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

    [SerializeField]
    private GameObject stepPrefab;

    #endregion

    #region Private fields


    private GameObject currentHighestStep;
    private StepController currentHighestStepController;

    #endregion

    #region Unity methods

    private void Start()
    {
        currentHighestStepController = FindObjectOfType<StepController>();
        currentHighestStep = currentHighestStepController.gameObject;
    }

    private void Update()
    {
        UpdateSteps();
    }
    #endregion

    #region Step Creation
    private void UpdateSteps()
    {
        float currentPlayerPos = BaseGameController.Instance.GetPlayerY();
        float currentHighestStepPos = currentHighestStepController.GetStepY();

        if (currentHighestStepPos - currentPlayerPos < stepCreationThreshold)
        {
            CreateNextStepRandom(currentHighestStepPos);
        }
    }

    private void CreateNextStepRandom(float currentHighestStepPos)
    {
        float min_y = currentHighestStepPos + spawnStepsMinY;
        float max_y = currentHighestStepPos + spawnStepsMaxY;

        float x = Random.Range(spawnStepsMinX, spawnStepsMaxX);
        float y = Random.Range(min_y, max_y);

        GameObject newStep = CreateStep(x, y);
       
        currentHighestStep = newStep;
        currentHighestStepController = currentHighestStep.GetComponent<StepController>();
    }

    public GameObject CreateStep(float x, float y)
    {
        GameObject newStep = Instantiate(stepPrefab, new Vector3(x, y, 0), Quaternion.identity);
        newStep.transform.parent = StepRoot.transform;

        return newStep;
    }

    #endregion
}
