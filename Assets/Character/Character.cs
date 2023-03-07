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
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRange = 0.5f;
    [SerializeField] protected LayerMask characterLayers;

    //[character stats]
    [SerializeField] protected Stats healthStat;
    protected Rigidbody2D rb;
    protected Animator myAnimator;
    protected abstract bool dead { get; }

    #region monos
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        healthStat.Initialise();
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

    public void Attack()
    { 
        //play attack animation
        myAnimator.SetTrigger("attack");
        //detect enemy
        Collider2D[] hitCharacter = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,characterLayers);
        //damage
        foreach (Collider2D character in hitCharacter)
        {
            StartCoroutine(character.GetComponent<Character>().TakeDamage());
            Debug.Log("HIT "+character.name);
        }
    }
    
    #endregion
    
    #region subMechanics

    public abstract IEnumerator TakeDamage();

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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    #endregion
}
