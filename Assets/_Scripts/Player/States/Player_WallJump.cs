using System;
using UnityEngine;

public class Player_WallJump : PlayerState
{
    private bool didWallJump = false;
    private float wallJumpTime = 0f;
    private float wallJumpGrace = 0.12f; // thời gian né tránh chuyển ngay sang wallSlide

    public override Type GroupType => typeof(Player_Aired);

    public Player_WallJump(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        didWallJump = false;
        wallJumpTime = -999f;
    }
    public override void ExitState()
    {
        base.ExitState();
        mPlayer.didJump = true;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToWallSlideOrIdle();
        ChangeToFallState();
    }
    public override void FixUpdateState()
    {
        if(!didWallJump)
        {
            ApplyWallJump();
            didWallJump = true;
            mPlayer.g_isJumping = true;
            wallJumpTime = Time.time; 
        }
        base.FixUpdateState();
    }
    private void ChangeToWallSlideOrIdle()
    {
        if(mPlayer.onWall && didWallJump && Time.time > wallJumpTime + wallJumpGrace)
        {
            this.mStateMachine.ChangeState(mPlayer.wallSlide);
            return;
        }
        else if(mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.idle);
            return;
        }
    }
    private void ApplyWallJump()
    {
        float facing = -mPlayer.facingDirection.x;
        mPlayer.SetVelocity(mPlayer.wallJumForce.x * facing, mPlayer.wallJumForce.y);
    }
    private void ChangeToFallState()
    {
        if(mPlayer.rb.linearVelocityY <= 0 && !mPlayer.onWall)
        {
            mPlayer.mStateMachine.ChangeState(mPlayer.fall);
            return;
        }
    }
}
