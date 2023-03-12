using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float playerspeed = 6.7f;
    public bool grounded;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float radOfCircle;
    public float thrust;

    protected override bool dead
    {
        get
        {
            return healthStat.CurrentVal < 1;
        }
    }

    public override void Start()
    {
        base.Start();
        speed = playerspeed;
    }

    private void Update()
    {
        //check vertical velocity
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("fall", true);
        }
        direction = Input.GetAxisRaw("Horizontal");
        Flip(direction);
        HandleMovement();
        HandleJumping();

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    public void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            TurnAround();
        }
    }

    private void HandleJumping()
    {
        //[HandleJumping]
        //grounded check
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOfCircle, whatIsGround);
        
        if (grounded)
        {
            myAnimator.ResetTrigger("isJump");
            myAnimator.SetBool("fall", false);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
            myAnimator.SetTrigger("isJump");
        }
    }
    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        Flip(horizontal);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public override IEnumerator TakeDamage()
    {
        healthStat.CurrentVal--;
        if (!dead)
        {
            Debug.Log("player hp at"+healthStat.CurrentVal);
        }
        else
        {
            Debug.Log("PLAYER DIED");
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOfCircle);
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
    }
}

