using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField]
    private float fallingSpeed;

    [SerializeField]
    private float fallingAccelation;

    private int currentLevel;

    [SerializeField]
    private int stepsPerLevel;

    public float FallingSpeed
    {
        get { return fallingSpeed; }
        set { fallingSpeed = value; }
    }

    void Start()
    {
        currentLevel = 1;
    }

    void FixedUpdate()
    {
        if (BaseGameController.Instance.StepsClimbed > stepsPerLevel * currentLevel)
        {
            currentLevel++;
            fallingSpeed += fallingAccelation;
        }
    }
}
