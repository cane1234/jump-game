using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Private fields
    private Rigidbody2D rigidBody2D;

    private bool playerInputEnabled;

    private LogicState currentLogicState;

    private bool collisionExit;

    private bool collisionEnter;

    private bool isInCollision;

    [Range(0, 100)]
    [SerializeField]
    private float fuel;
    #endregion

    #region Editor Fields
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float jetpackForce;

    [SerializeField]
    private Animator PlayerAnimator;

    [SerializeField]
    private List<RuntimeAnimatorController> AnimatorControllers;

    [SerializeField]
    private SpriteRenderer PlayerSpriteRenderer;

    [SerializeField]
    private float fuelConsumption;
    #endregion

    private enum LogicState
    {
        Jumping, Standing, Walking, Jetpack
    }

    #region Unity methods

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.freezeRotation = true;
        playerInputEnabled = true;

        fuel = 100;

        EnterJumpingState();

    }

    private void Update()
    {
        if (!playerInputEnabled)
        {
            return;
        }

        bool isThereHorizontalMovement = ApplyHorizontalMovement();

        HandleState(isThereHorizontalMovement);

        ResetCollisionStates();
        
    }

    #endregion

    #region Public methods
    public bool PlayerInputEnabled
    {
        get { return playerInputEnabled; }
        set { playerInputEnabled = value; }
    }

    #endregion

    #region Collision

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Step") || collision.gameObject.name.Contains("Floor"))
        {
            collisionEnter = true;
            isInCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Step") || collision.gameObject.name.Contains("Floor"))
        {
            collisionExit = true;
            isInCollision = false;
        }
    }

    private void ResetCollisionStates()
    {
        collisionEnter = false;
        collisionExit = false;
    }
    #endregion

    #region Animations
    private void TurnLeftAnimation()
    {
        PlayerSpriteRenderer.flipX = true;
    }

    private void TurnRightAnimation()
    {
        PlayerSpriteRenderer.flipX = false;
    }

    private void StandAnimation()
    {
        PlayerAnimator.runtimeAnimatorController = null;
    }

    private void JumpAnimation()
    {
        PlayerAnimator.runtimeAnimatorController = AnimatorControllers[0];
    }

    private void WalkAnimation()
    {
        PlayerAnimator.runtimeAnimatorController = AnimatorControllers[1]; 
    }
    #endregion

    #region State Handling

    private void HandleState(bool isThereHorizontalMovement)
    {
        switch (currentLogicState)
        {
            case LogicState.Standing:
                HandleStandingState(isThereHorizontalMovement);
                break;
            case LogicState.Walking:
                HandleWalkingState(isThereHorizontalMovement);
                break;
            case LogicState.Jumping:
                HandleJumpingState();
                break;
            case LogicState.Jetpack:
                HandleJetpackState();
                break;
        }
    }

    private void HandleJetpackState()
    {
        
        if (!Input.GetKey(KeyCode.W) || fuel <= 0)
        {
            if (!isInCollision)
            {
                EnterJumpingState();
            }
            else
            {
                EnterStandingState();
            }
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(0, jetpackForce * Time.deltaTime));
            fuel -= fuelConsumption * Time.deltaTime;
        }
    }

    private void HandleStandingState(bool isThereHorizontalMovement)
    {
        if (isThereHorizontalMovement)
        {
            EnterWalkingState();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
            EnterJumpingState();
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody2D.AddForce(new Vector2(0, jetpackForce * Time.deltaTime));
            EnterJetpackState();
        }
    }

    private void HandleWalkingState(bool isThereHorizontalMovement)
    {
        if (!isThereHorizontalMovement)
        {
            EnterStandingState();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
            EnterJumpingState();
        }

        if (collisionExit)
        {
            EnterJumpingState();
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody2D.AddForce(new Vector2(0, jetpackForce * Time.deltaTime));
            EnterJetpackState();
        }
    }

    private void HandleJumpingState()
    {
        if (collisionEnter)
        {
            EnterStandingState();
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody2D.AddForce(new Vector2(0, jetpackForce * Time.deltaTime));
            EnterJetpackState();
        }
    }

    private void EnterStandingState()
    {
        currentLogicState = LogicState.Standing;

        StandAnimation();

        Debug.Log("Entering stand");
    }

    private void EnterWalkingState()
    {
        currentLogicState = LogicState.Walking;

        WalkAnimation();

        Debug.Log("Entering walk");
    }

    private void EnterJumpingState()
    {
        currentLogicState = LogicState.Jumping;

        JumpAnimation();

        Debug.Log("Entering jump");
    }

    private void EnterJetpackState()
    {
        currentLogicState = LogicState.Jetpack;

        JumpAnimation();

        Debug.Log("Entering Jetpack");
    }
    #endregion

    #region Horizontal Movement
    private void HandleTurning(float movement)
    {
        if(movement > 0)
        {
            TurnRightAnimation();
        } 
        else if(movement < 0)
        {
            TurnLeftAnimation();
        }
    }

    private bool ApplyHorizontalMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);

        HandleTurning(moveHorizontal);
        rigidBody2D.AddForce(movement * speed * Time.deltaTime);

        if(moveHorizontal != 0)
        {
            return true;
        }

        return false;
    }
    #endregion
}
