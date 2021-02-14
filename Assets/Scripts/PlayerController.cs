using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Private fields
    private Rigidbody2D rigidBody2D;

    private bool playerInputEnabled;
    #endregion

    #region Editor Fields
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;
    #endregion

    #region Unity methods

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerInputEnabled = true;
    }

    private void Update()
    {
        if (playerInputEnabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(moveHorizontal, 0);

            rigidBody2D.AddForce(movement * speed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody2D.AddForce(new Vector2(0, jumpForce));
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

}
