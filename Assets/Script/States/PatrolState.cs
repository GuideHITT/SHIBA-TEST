using System.Collections;
using System.Collections.Generic;
using Script.States;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDur;

    public void Action()
    {
        Patrol();

        enemy.Move();

        if (enemy.Target != null && enemy.VisibleRange)
        {
            enemy.ChangeState(new EnterRangeState());
        }
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log("Enemy is patrolling");
        patrolTimer = 0f;
        patrolDur = Random.Range(5, 10);
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolDur) enemy.ChangeState(new IdleState());
    }

    public void Exit()
    {
        //do nothing
    }
}
