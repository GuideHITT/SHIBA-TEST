using UnityEngine;

namespace Script.States
{
    public class EnterRangeState : IEnemyState
    {
        private Enemy enemy;

        public void Enter(Enemy enemy)
        {
            Debug.Log("Enemy is moving closer");
            this.enemy = enemy;
        }
    
        public void Action()
        {
            //if in melee range to attack
            if (enemy.InMeleeRange) enemy.ChangeState(new MeleeState());
            //if not
            else if (enemy.Target != null) enemy.Move();
            //if can't see player
            else enemy.ChangeState(new IdleState());
            
        }
        public void Exit()
        {
        
        }
    }
}
