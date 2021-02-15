using UnityEngine;

public class BottomEdge : MonoBehaviour
{
    #region Collision Names

    private const string playerName = "Player";

    private const string stepName = "Step";

    #endregion

    #region Private Fields

    private BoxCollider2D boxCollider;

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Private Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == playerName)
        {
            BaseGameController.Instance.EndGame();
        }
        if (collision.gameObject.name.Contains(stepName))
        {
            Destroy(collision.gameObject);
        }
    }

    #endregion

}
