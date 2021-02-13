using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    public float speed;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
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
