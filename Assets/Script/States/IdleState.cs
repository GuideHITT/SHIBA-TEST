using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float idleTimer;
    private float idleDur;
    
    public void Action()
    {
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("ENEMY IS IDLE");
        idleTimer = 0;
        idleDur = Random.Range(1, 10);
    }

    private void Idle()
    {
        enemy.myAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDur)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }
}
