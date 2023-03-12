using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : Character
{
    private IEnemyState currentState;

    public GameObject Target { get; set; }
    [SerializeField]private float meleeRange;
    [SerializeField]private float visibleRange;
    [SerializeField]private Transform leftEdge, rightEdge;
    [SerializeField]private Vector3 distanceToPlayer;
    
    private SpriteRenderer spRend;

    public Transform player;

    protected override bool dead
    {
        get
        {
            return healthStat.CurrentVal <= 0;
        }
    }

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
        
    }

    public bool VisibleRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= visibleRange;
            }
            return false;
        }
    }

    #region monos

    public override void Start()
    {
        base.Start();
        spRend = GetComponent<SpriteRenderer>();
        ChangeState(new IdleState());
    }

    public void Update()
    {
        if (!dead)
        {
            if (!TakingDamage)
            {
                currentState.Action();
            }
            LookAtTarget();
        }
        distanceToPlayer.x = player.transform.position.x - transform.position.x;
    }

    #endregion

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null) currentState.Exit();
        {
            currentState = newState;
            currentState.Enter(this);
        }
    }

    public override IEnumerator TakeDamage(float KBForce)
    {
        healthStat.CurrentVal--;
        if (!dead)
        {
            Debug.Log("Enemy hp at"+healthStat.CurrentVal);
        }
        else
        {
            Debug.Log("DIED");
            Destroy(gameObject);
        }
        yield return null;
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }
    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                TurnAround();
            }
        }
    }
    public void Move()
    {
        if (!dead)
        {
            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                myAnimator.SetFloat("speed", 1);
                transform.Translate(GetDirection() * (speed * Time.deltaTime));
            }
            else if (currentState is PatrolState)
            {
                TurnAround();
            }
        }
    }

    public void EnemyAttack()
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftEdge.position, 1f);
        Gizmos.DrawWireSphere(rightEdge.position, 1f);
    }
}
