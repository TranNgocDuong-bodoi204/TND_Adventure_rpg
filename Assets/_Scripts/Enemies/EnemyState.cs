using System.Collections;
using UnityEngine;

public class EnemyState : EntityState
{
    protected EnemyContext enemy;
    public EnemyState(StateMachine stateMachine, string animationParam, EnemyContext context) : base(stateMachine, animationParam)
    {
        this.mAnimator = context.animator;
        this.enemy = context;

    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToFallBack();
    }

    protected virtual void ChangeToFallBack()
    {
        if(GetDistanceToPlayer() <= enemy.minDistanceNeedFallBack)
        {
            mStateMachine.ChangeState(enemy.fallBack);
            enemy.testButton = false;
            return;
        }
    }
    protected virtual Vector2 GetDirectionToPlayer()
    {
        Vector2 dir = enemy.playerReference.position - enemy.eyePosition.position;
        return dir.normalized;
    }
    protected virtual float GetDistanceToPlayer()
    {
        return Vector2.Distance(enemy.playerReference.position, enemy.eyePosition.position);
    }

}
