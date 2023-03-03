using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    public bool grounded;
    private Rigidbody2D rb;
    [SerializeField]private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOfCircle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position,radOfCircle,whatIsGround);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position,radOfCircle);
    }
}
