using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    float runAcceleration = 3;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    bool running = false;

    Controller2D controller;
    Animator animator;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("immo", true);

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            animator.SetBool("immo", true);
            animator.SetBool("jump", false);
            velocity.y = 0;
        }

        // MOVEMENT
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x != 0 && animator.GetBool("jump") != true)
        {
            animator.SetBool("immo", false);
            animator.SetBool("walk", true);
            transform.localScale = new Vector3(0.5f * Mathf.Sign(input.x), 0.5f, 0.5f);
        }
        if (input.x == 0)
        {
            animator.SetBool("walk", false);
            animator.SetBool("immo", true);
            if (running)
            {
                animator.SetBool("run", false);
            }
        }

        if (input.x != 0 && running)
        {
            animator.SetBool("immo", false);
            animator.SetBool("walk", false);
            animator.SetBool("run", true);
        }


        // JUMP
        if (Input.GetButton("Jump") && controller.collisions.below)
        {
            animator.SetBool("immo", false);
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
            animator.SetBool("jump", true);
            velocity.y = jumpVelocity;
            if (running)
            {
                velocity.y += 2;
            }
        }

        // RUN
        if (Input.GetButton("Run") && controller.collisions.below && !running)
        {
            moveSpeed += runAcceleration;
            running = true;
        }
        if (Input.GetButtonUp("Run") && running)
        {
            animator.SetBool("run", false);
            if (input.x != 0)
            {
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("immo", true);
            }
            moveSpeed -= runAcceleration;
            running = false;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
