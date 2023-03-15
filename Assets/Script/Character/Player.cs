using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : Character
{
    [SerializeField] protected float radOfCircle;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsGround;
    public bool grounded;
    private float nextAttackTime;
    private float attackRate = 2;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    
    private float playerspeed = 6.7f;
    public ParticleSystem dust;
    [SerializeField] protected float kbForceUp = 0.5f;

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

        Direction = Input.GetAxisRaw("Horizontal");
        Flip(Direction);
        HandleMovement();
        HandleJumping();

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void Flip(float horizontal)
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
            coyoteTimeCounter = coyoteTime;
            myAnimator.ResetTrigger("isJump");
            myAnimator.SetBool("fall", false);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            CreateDust();
            Jump();
            myAnimator.SetTrigger("isJump");
            coyoteTimeCounter = 0f;
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
        myAnimator.SetTrigger("hurt");
        healthStat.CurrentVal--;
        if (!dead)
        {
            KnockBack();
            Debug.Log("player hp at"+healthStat.CurrentVal);
        }
        else
        {
            Debug.Log("PLAYER DIED");
        }
        yield return null;
    }
    public void CreateDust()
    {
        dust.Play();
    }

    private void KnockBack()
    {
        Transform attacker = GETClosestDamageSource();
        Vector2 knockBackDirec = new Vector2(transform.position.x - attacker.transform.position.x, 0);
        rb.velocity = new Vector2(knockBackDirec.x, kbForceUp) * KBForce;
    }

    private Transform GETClosestDamageSource()
    {
        GameObject[] damageSource = GameObject.FindGameObjectsWithTag("enemy");
        float closestDist = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach (GameObject go in damageSource)
        {
            float currentDist;
            currentDist = Vector3.Distance(transform.position, go.transform.position);
            if (currentDist < closestDist)
            {
                closestDist = currentDist;
                currentClosestDamageSource = go.transform;
            }
        }

        return currentClosestDamageSource;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOfCircle);
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
    }
}

