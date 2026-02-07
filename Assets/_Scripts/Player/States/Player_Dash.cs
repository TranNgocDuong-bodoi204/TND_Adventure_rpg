using Unity.VisualScripting;
using UnityEngine;

public class Player_Dash : PlayerState
{
    private float enterTime;
    private float originalGravity;
    private PlayerContext playerContext;

    public Player_Dash(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        originalGravity = mPlayer.rb.gravityScale;
        mPlayer.rb.gravityScale = 0;

        enterTime = Time.time;
        mPlayer.canChangeState = false;
        SetCoolDown(mPlayer.dashCoolDown);
    }
    public override void ExitState()
    {
        base.ExitState();
        mPlayer.rb.gravityScale = originalGravity;
        mPlayer.rb.linearVelocity = Vector2.zero;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        StopDashWhenHitWall();

        if(Time.time > enterTime  + mPlayer.dashDuration) mPlayer.canChangeState = true;

        ChangeToIdle();
        ChangeToWallSlide();
        ChangeToFall();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        DashMoving();
    }
    private void ChangeToIdle()
    {
        // chuyển idle khi chạm tường khi ở dưới đất
        if(mPlayer.onWall && mPlayer.canChangeState && mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.wallSlide);
            return;
        }
        // chuyển idle khi ở dưới đất
        else if(mPlayer.canChangeState && mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.idle);
            return;
        }
    }

    protected void ChangeToFall()
    {
        if(mPlayer.canChangeState && !mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.fall);
            return;
        }
    }

    private void DashMoving()
    {
        mPlayer.SetVelocity(mPlayer.dashMoveSpeed*mPlayer.facingDirection.x, 0);
    }
    private void ChangeToWallSlide()
    {
        if(mPlayer.onWall && !mPlayer.onGround && mPlayer.canChangeState)
        {
            this.mStateMachine.ChangeState(mPlayer.wallSlide);
            return;
        }
    }
    private void StopDashWhenHitWall()
    {
        if(mPlayer.onWall)
        {
            mPlayer.canChangeState = true;
        }
    }
}
