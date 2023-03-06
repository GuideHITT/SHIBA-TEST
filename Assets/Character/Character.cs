using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{ 
    //[Movement variable]
    [SerializeField] protected float speed = 2.0f;
    protected float direction;
    protected bool facingRight = true;
    
    //[Jump variable]
    //jump details//
    [SerializeField]protected float jumpForce;

    //groundCheck//
    public bool grounded;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float radOfCircle;

    //[attack variable]

    //[character stats]
    protected Rigidbody2D rb;
    protected Animator myAnimator;
 
    #region monos
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        //handle input
        
        //[HandleJumping]
        //grounded check
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOfCircle, whatIsGround);
        
        //check vertical velocity
        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("fall", true);
        }
    }

    public virtual void FixedUpdate()
    {
        //handle mechanics/physics
        HandleMovement();
    }

    #endregion

    #region mechanics

    protected void Move()
    {
        rb.velocity = new Vector2(direction * speed,rb.velocity.y);
    }

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    #endregion
    
    #region subMechanics

    protected abstract void HandleJumping();
    
    
    protected void TurnAround(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    
    protected virtual void HandleMovement()
    {
        Move();
    }

    #endregion

    #region visDebug

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOfCircle);
    }
    #endregion
}
