using UnityEngine;

public class StepController : MonoBehaviour
{
    #region Private Fields

    private BoxCollider2D boxCollider;

    private bool climbed;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        climbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(BaseGameController.Instance.GetPlayerY() > GetStepY())
        {
            boxCollider.enabled = true;
            if (!climbed)
            {
                climbed = true;
                BaseGameController.Instance.StepsClimbed ++; 
            }
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Used to check if the player is above this step so it can turn on/off its collider.
    /// </summary>
    /// <returns> The Y coordinate of the lowest point of this Step. </returns>
    public float GetStepY()
    {
        Bounds stepBounds = this.gameObject.GetComponent<BoxCollider2D>().bounds;
        float stepTop = stepBounds.center.y + stepBounds.extents.y;

        return stepTop;
    }
    #endregion
}
