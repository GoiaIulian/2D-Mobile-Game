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


    float timeToWallunstik;
    float timeToClimb;
    Vector2 movement;

    
    float gravity;

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

        gravity = -(2 * PlayerStats.MaxjumpHeight) / Mathf.Pow(PlayerStats.TimeToJumpApex, 2);
        maxjumpVelocity = Mathf.Abs(gravity) * PlayerStats.TimeToJumpApex;
        minjumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * PlayerStats.MinJumpHeight);
    }

    private void FixedUpdate()
    {
        Movement();
        wallDirX = (controller.collisions.left) ? -1 : 1;


        float targetVelocityX = movement.x * PlayerStats.MoveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? PlayerStats.AccelerationTimeGrounded : PlayerStats.AccelerationTimeAirborn);


        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -PlayerStats.WallSlideSpeedMax)
            {
                velocity.y = -PlayerStats.WallSlideSpeedMax;
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
                    timeToWallunstik = PlayerStats.WallStickTime;
                }
            }
            else
            {
                timeToWallunstik = PlayerStats.WallStickTime;
            }
        }

        if (timeToClimb > 0)
        {
            timeToClimb -= Time.fixedDeltaTime;
        }

        velocity.y += gravity * Time.fixedDeltaTime;

        if (velocity.y < -PlayerStats.MaxFallSpeed)
        {
            velocity.y = -PlayerStats.MaxFallSpeed;
        }

        if (velocity.y < 0 && controller.collisions.below)
        {
            velocity.y = -.1f;
        }


        controller.collisions.wasAirborn = !grounded;

        UpdateAnimations();

        if (grounded)
        {
            PlayerStats.MoveSpeed = 8;
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
            PlayerStats.MoveSpeed = 10;

            if (timeToClimb > 0)
            {
                return;
            }

            timeToClimb = PlayerStats.ClimbTimeout;

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
        else if (PlayerStats.CanJump && timeToClimb <= 0)
        {
            velocity.y = minjumpVelocity;
            timeToClimb = PlayerStats.ClimbTimeout;
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