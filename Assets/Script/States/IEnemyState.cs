using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Action();
    void Enter(Enemy enemy);
    void Exit();
}