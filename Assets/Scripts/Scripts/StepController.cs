using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(BaseGameController.Instance.GetPlayerY() > GetStepY())
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
    #endregion

    #region Helper Methods
    private float GetStepY()
    {
        Bounds stepBounds = this.gameObject.GetComponent<BoxCollider2D>().bounds;
        float stepTop = stepBounds.center.y + stepBounds.extents.y;

        return stepTop;
    }
    #endregion
}
