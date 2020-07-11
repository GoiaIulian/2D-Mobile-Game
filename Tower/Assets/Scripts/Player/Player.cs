using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public Vector2 wallClimb;
    public Vector2 wallHop;
    public Vector2 wallLeap;
    public bool grounded;


    bool wallSliding = false;

    float wallStikTime = .45f;
    float timeToWallunstik;
    Vector2 movement;

    float maxFallSpeed = 20f;


    public float maxjumpHeight = 4;
    public float minJumpHeight = 1;

    public float timeToJumpApex = .4f;

    public float wallSlideSpeedMax = 3f;

    float moveSpeed = 10;
    float gravity;

    float accelerationTimeAirborn = .2f;
    float accelerationTimeGrounded = .1f;

    float maxjumpVelocity;
    float minjumpVelocity;
    float velocityXSmoothing;
    Vector3 velocity;
    int wallDirX;


    PlayerControlls input;
    Controller2D controller;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();

        controller = GetComponent<Controller2D>();

        input = new PlayerControlls();

        input.Controlls.TakeOff.performed += _ => TakeOff();
        input.Controlls.Jump.performed += _ => Climb();

        gravity = -(2 * maxjumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxjumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minjumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void FixedUpdate()
    {
        Movement();
        wallDirX = (controller.collisions.left) ? -1 : 1;


        float targetVelocityX = movement.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborn);


        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            velocityXSmoothing = 0;
            velocity.x = 0;

            if (timeToWallunstik > 0)
            {

                if (movement.x != wallDirX && movement.x != 0)
                {
                    timeToWallunstik -= Time.fixedDeltaTime;
                }
                else
                {
                    timeToWallunstik = wallStikTime;
                }
            }
            else
            {
                timeToWallunstik = wallStikTime;
            }
        }

        velocity.y += gravity * Time.fixedDeltaTime;

        if (velocity.y < -maxFallSpeed)
        {
            velocity.y = -maxFallSpeed;
        }

        if (velocity.y < 0 && controller.collisions.below)
        {
            velocity.y = -.1f;
        }


        controller.collisions.wasAirborn = !grounded;

        UpdateAnimations();

        if (grounded)
        {
            moveSpeed = 10;
        }
        grounded = controller.collisions.below;

        controller.Move(velocity * Time.fixedDeltaTime, false);
    }

    void Movement()
    {
        if (Touch.activeFingers.Count >= 1)
        {
            Touch activeTouch = Touch.activeFingers[0].currentTouch;

            if (activeTouch.screenPosition.x > Screen.width / 2)
            {
                MovementInput(new Vector2(1, 0));
            }
            else
            {
                MovementInput(new Vector2(-1, 0));
            }
        }
        else
        {
            if (movement.x > 0.1)
            {
                MovementInput(new Vector2(movement.x - .1f, 0));
            }
            else if (movement.x < -0.1)
            {
                MovementInput(new Vector2(movement.x + .1f, 0));
            }
            else
            {
                MovementInput(new Vector2(0, 0));
            }
        }
    }

    void Jump()
    {
        if (wallSliding)
        {
            moveSpeed = 10;
            if (wallDirX == movement.x)
            {
                velocity.x = -wallDirX * wallClimb.x;
                velocity.y = wallClimb.y;

            }

            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        else if (grounded)
        {
            velocity.y = maxjumpVelocity;
        }
        //else if (controller.collisions.canDoubleJump)
        //{
        //    velocity.y = maxjumpVelocity * .8f;
        //    controller.collisions.canDoubleJump = false;
        //}
    }

    private void TakeOff()
    {
        if (grounded)
        {
            Jump();
        }
    }

    private void Climb()
    {
        if(!grounded)
        {
            Jump();
        }
    }

    void JumpCancel()
    {
        if (velocity.y > minjumpVelocity)
        {
            velocity.y = minjumpVelocity;
        }
    }

    void MovementInput(Vector2 m)
    {
        movement.y = 0;
        if (m.x != 0)
        {
            movement.x = m.x;
        }
        else
        {
            movement.x = 0;
        }
    }

    void UpdateAnimations()
    {
    }

    private void OnEnable()
    {
        input.Controlls.Enable();
    }

    private void OnDisable()
    {
        input.Controlls.Disable();
    }

}