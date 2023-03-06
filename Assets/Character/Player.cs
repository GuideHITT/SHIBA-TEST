using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float playerspeed = 6.7f;
    
    public override void Start()
    {
        base.Start();
        speed = playerspeed;

    }
    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        myAnimator.SetFloat("speed",Mathf.Abs(direction));
        TurnAround(direction);
    }

    protected override void HandleJumping()
    {
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
}

