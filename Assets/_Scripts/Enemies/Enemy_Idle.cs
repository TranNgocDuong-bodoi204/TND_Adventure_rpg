using UnityEngine;

public class Enemy_Idle : EnemyState
{
    private float idleTime = .6f;
    private float enterTime;
    public Enemy_Idle(StateMachine stateMachine, string animationParam, EnemyContext context) : base(stateMachine, animationParam, context)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter Idle State");
        enterTime = Time.time;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Rest();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        ChangeToChase();
        ChangeToAttack();
    }

    private void Rest()
    {
        bool restOver = Time.time >= enterTime + idleTime;
        if(restOver && !enemy.isPlayerDetected)
        {
            ChangeToMove();
            return;
        }
    }

    private void ChangeToAttack()
    {
        if(enemy.PlayerInAttackRange 
        && enemy.isCanAttack 
        && !enemy.animTrigger 
        && enemy.timer <= 0)
        {
            mStateMachine.ChangeState(enemy.attack);
            return;
        }
    }
    private void ChangeToChase()
    {
        if(enemy.isPlayerDetected && !enemy.PlayerInAttackRange)
        {
            mStateMachine.ChangeState(enemy.chase);
            return;
        }
    }

    private void ChangeToMove()
    {
        enemy.SetFacingDirection(-enemy.facingDirection);
        enemy.FlipCharacter();
        mStateMachine.ChangeState(enemy.move);
    }
}
