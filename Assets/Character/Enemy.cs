using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected override bool dead
    {
        get
        {
            return healthStat.CurrentVal <= 0;
        }
    }

    public override IEnumerator TakeDamage()
    {
        healthStat.CurrentVal--;
        if (!dead)
        {
            Debug.Log("Enemy hp at"+healthStat.CurrentVal);
        }
        else
        {
            Debug.Log("DIED");
        }
        yield return null;
    }

    protected override void HandleJumping()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOfCircle);
    }
}
