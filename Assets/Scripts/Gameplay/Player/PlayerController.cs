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
    #endregion

    #region Editor Fields
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Animator PlayerAnimator;

    [SerializeField]
    private List<RuntimeAnimatorController> AnimatorControllers;

    [SerializeField]
    private SpriteRenderer PlayerSpriteRenderer;
    #endregion

    private enum LogicState
    {
        Jumping, Standing, Walking
    }

    #region Unity methods

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.freezeRotation = true;
        playerInputEnabled = true;
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Step"))
        {
            collisionExit = true;
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
    }

    private void HandleJumpingState()
    {
        if (collisionEnter)
        {
            EnterStandingState();
        }
    }

    private void EnterStandingState()
    {
        currentLogicState = LogicState.Standing;

        StandAnimation();
    }

    private void EnterWalkingState()
    {
        currentLogicState = LogicState.Walking;

        WalkAnimation();
    }

    private void EnterJumpingState()
    {
        currentLogicState = LogicState.Jumping;

        JumpAnimation();
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
