using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField]
    private float fallingSpeed;

    [SerializeField]
    private float fallingAccelation;

    public float FallingSpeed
    {
        get { return fallingSpeed; }
        set { fallingSpeed = value; }
    }

    void FixedUpdate()
    {
        
    }
}
