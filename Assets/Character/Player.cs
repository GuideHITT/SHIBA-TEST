using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float playerspeed = 6.7f;

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
    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
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

    public override IEnumerator TakeDamage()
    {
        healthStat.CurrentVal--;
        if (!dead)
        {
            Debug.Log("player hp at"+healthStat.CurrentVal);
        }
        else
        {
            Debug.Log("DIED");
        }
        yield return null;
    }
}

