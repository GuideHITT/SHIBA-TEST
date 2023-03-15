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
    protected float Direction;
    protected bool facingRight = true;

    //[Jump variable]
    //jump details//
    [SerializeField]protected float jumpForce;


    //[attack variable]
    [SerializeField] protected float attackRange = 0.5f;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] public float KBForce;
    public Transform attackCheck;

    //[character stats]
    public string characterType;
    [SerializeField] protected Stats healthStat;
    [field: SerializeField] public bool TakingDamage { get; set; }
    [field: SerializeField]public Animator myAnimator { get; private set; }
    protected Rigidbody2D rb;
    protected abstract bool dead { get; }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        healthStat.Initialise();
    }

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    protected void Attack()
    {
        //play attack animation
        myAnimator.SetTrigger("attack");
        //detect enemy
        Collider2D target = Physics2D.OverlapCircle(attackCheck.position,attackRange,whatIsTarget);
        //damage
        if(target != null)
        {
            Debug.Log(characterType + " attacked " +target.GetComponent<Character>().characterType);
            StartCoroutine(target.GetComponent<Character>().TakeDamage());
        }
    }
    public abstract IEnumerator TakeDamage();
    protected void TurnAround()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, 1);
    }
}
