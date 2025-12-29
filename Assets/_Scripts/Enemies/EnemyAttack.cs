using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    private int attackCount = 2;
    

    public EnemyAttack(StateMachine stateMachine, string animationParam, EnemyContext context) : base(stateMachine, animationParam, context)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter Attack State");
        enemy.isCanAttack = false;
        enemy.isInBattle = true;
        Debug.Log("Enemy is in battle: " + enemy.isInBattle);
        SetAttackIndex();
        enemy.animator.SetFloat(enemy.PARAM_ATTACKINDEX, enemy.attackIndex);
    }
    public override void ExitState()
    {
        base.ExitState();
        enemy.animTrigger = false;
        enemy.isCanAttack = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        ChangeToIdle();
        ChangeToChase();
    }
    private void ChangeToIdle()
    {
        if(enemy.animTrigger == true)
        {
            mStateMachine.ChangeState(enemy.idle);
            enemy.SetTimer(enemy.attackTimeCooldown);
            return;
        }
    }
    private void ChangeToChase()
    {
        if(enemy.PlayerInAttackRange == false && enemy.animTrigger)
        {
            mStateMachine.ChangeState(enemy.chase);
            return;
        }
    }


    private void SetAttackIndex()
    {
        if(enemy.attackIndex >= attackCount - 1)
        {
            enemy.attackIndex = 0;
        }
        else
        {
            enemy.attackIndex++;
        }
    }
}