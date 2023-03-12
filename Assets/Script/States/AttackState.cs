using System.Collections;
using System.Collections.Generic;
using Script.States;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private float attackTimer;
    private float attackCooldown = 3;
    private bool canAttack = true;
    private Enemy enemy;
    
    public void Action()
    {
        Attack();

        if (enemy.VisibleRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(new EnterRangeState());
        }
        else if (enemy.Target == null)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Enter(Enemy enemy)
    {
        Debug.Log("Enemy: Meleeing");
        this.enemy = enemy;
        if (enemy.VisibleRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(new EnterRangeState());
        }
        else if (enemy.Target == null)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }
    
    public void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer>=attackCooldown)
        {
            canAttack = true;
            Debug.Log("Enemy: Can Attack Now");
            attackTimer = 0;
        }
        if (canAttack)
        {
            enemy.EnemyAttack();
            canAttack = false;
            Debug.Log("Enemy: Hitting");
            enemy.myAnimator.SetTrigger("attack");
        }
    }
}
