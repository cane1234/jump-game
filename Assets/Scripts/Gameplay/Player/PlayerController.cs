using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Private fields
    private Rigidbody2D rigidBody2D;

    private bool playerInputEnabled;

    private LogicState currentLogicState;
    #endregion

    #region Editor Fields
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    #endregion

    private enum LogicState
    {
        Jumping, Standing
    }

    #region Unity methods

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.freezeRotation = true;
        playerInputEnabled = true;
        currentLogicState = LogicState.Standing;
    }

    private void Update()
    {
        if (playerInputEnabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(moveHorizontal, 0);

            rigidBody2D.AddForce(movement * speed * Time.deltaTime);

            if (currentLogicState == LogicState.Standing)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigidBody2D.AddForce(new Vector2(0, jumpForce));
                    currentLogicState = LogicState.Jumping;
                }
            }
        }
    }

    #endregion

    #region Public methods
    public bool PlayerInputEnabled
    {
        get { return playerInputEnabled; }
        set { playerInputEnabled = value; }
    }

    #endregion

    #region Private Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Step"))
        {
            currentLogicState = LogicState.Standing;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Step"))
        {
            currentLogicState = LogicState.Jumping;
        }
    }
    #endregion
}
