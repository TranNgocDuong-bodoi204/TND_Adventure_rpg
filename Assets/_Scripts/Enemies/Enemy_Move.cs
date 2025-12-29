using UnityEngine;

public class Enemy_Move : EnemyState
{
    private float delayChange = .1f;
    private float timerStart = -1f;
    public Enemy_Move(StateMachine stateMachine, string animationParam, EnemyContext context) : base(stateMachine, animationParam, context)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.moveDirection = enemy.facingDirection;
        timerStart = -1f;
        Debug.Log("Enter Move State");
    }
    public override void ExitState()
    {
        base.ExitState();
        timerStart = -1f;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToIdle();
        ChangeToChase();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        DoMove();
    }

    private void DoMove()
    {
        enemy.SetVelocity(enemy.moveDirection.x * enemy.g_moveSpeed, enemy.rb.linearVelocity.y);
    }

    private void ChangeToChase()
    {
        if(enemy.isPlayerDetected)
        {
            mStateMachine.ChangeState(enemy.chase);
            return;
        }
    }

    private void ChangeToIdle()
    {
        // don't interrupt patrol if player detected (player handling should be elsewhere)
        if (enemy.isPlayerDetected)
        {
            timerStart = -1f;
            return;
        }

        // detected wall or no ground ahead -> start/accumulate delay timer
        if(!enemy.onGround || enemy.onWall)
        {
            if (timerStart < 0f) timerStart = Time.time;

            // only change state after the delay to prevent quick flicker when touching collider for one frame
            if (Time.time - timerStart >= delayChange)
            {
                mStateMachine.ChangeState(enemy.idle);
                enemy.rb.linearVelocity = Vector2.zero;
                timerStart = -1f;
            }
            return;
        }

        // reset timer if nothing detected
        timerStart = -1f;
    }
}
