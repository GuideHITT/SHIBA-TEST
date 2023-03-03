using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // animation & physics
    private Rigidbody2D rb2d;
    private Animator shibaAnimator;
    private bool facingRight = true;
    
    //player variables
    public float speed = 2.0f;
    public float horizMovement;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        shibaAnimator = GetComponent<Animator>();
    }
    
    //input for physics
    private void Update()
    {
        //check if player has input
        horizMovement = Input.GetAxisRaw("Horizontal");
        
    }

    //running the physics
    private void FixedUpdate()
    {
        //move the character
        rb2d.velocity = new Vector2(horizMovement * speed,rb2d.velocity.y);
        Flip(horizMovement);
        
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
