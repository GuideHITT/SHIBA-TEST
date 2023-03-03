using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{
    //jump details//
    public float jumpForce;

    //groundCheck//
    public bool grounded;
    [SerializeField]private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOfCircle;
    
    //component//
    private Rigidbody2D rb;
    private Animator jumpAnimator;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //grounded check
        grounded = Physics2D.OverlapCircle(groundCheck.position,radOfCircle,whatIsGround);
        if (grounded)
        {
            jumpAnimator.ResetTrigger("isJump");
            jumpAnimator.SetBool("fall",false);
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            jumpAnimator.SetTrigger("isJump");
        }

        if (rb.velocity.y < 0)
        {
            jumpAnimator.SetBool("fall",true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position,radOfCircle);
    }
}
