using System;
using UnityEngine;

public class Player_Aired : PlayerState
{
    private float minJumpTime = 0.1f;
    private float timeEnter;
    protected bool canChangeState;
    public override Type GroupType => typeof(Player_Aired);
    
    
    public Player_Aired(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {  
    }
    public override void EnterState()
    {
        if(mPlayer.mStateMachine.previousState.GroupType != this.GroupType) 
            mPlayer.didJump = false;
        base.EnterState();
        canChangeState = false;
        timeEnter = Time.time; 
    }
    public override void UpdateState()
    {
        if(Time.time > timeEnter+minJumpTime)
        {
            canChangeState = true;
        }
        base.UpdateState();
        ChangeToWallSlide();
        ChangeToIdleOrRun();
        ChangeToAirJump();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        ChangeToFall();
        SetYVelocityParam();
        MovingInAir();
    }
    private void ChangeToIdleOrRun()
    {
        if (canChangeState && !mPlayer.g_isJumping)
        {
            if (mPlayer.onGround && mPlayer.g_moveInput.x == 0)
            {
                this.mStateMachine.ChangeState(mPlayer.idle);
            }
            else if (mPlayer.onGround && mPlayer.g_moveInput.x != 0)
            {
                this.mStateMachine.ChangeState(mPlayer.run);
            }
        }
    }
    protected void SetYVelocityParam()
    {
        mAnimator.SetFloat(this.mPlayer.Y_VELOCITY_PARAM, mPlayer.rb.linearVelocityY);
    }
    private void ChangeToAirJump()
    {
        if(mPlayer.p_inputActions.Player.Jump.WasPressedThisFrame() && mPlayer.didJump ||
           mPlayer.p_inputActions.PlayerMobile.Jump.WasPressedThisFrame() && mPlayer.didJump )
        {
            this.mStateMachine.ChangeState(mPlayer.airJump);
            return;
        }
    }
    private void ChangeToWallSlide()
    {
        if(mPlayer.onWall && !mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.wallSlide);
        }
    }
    
    protected virtual void ChangeToFall()
    {
        if(!mPlayer.onGround && mPlayer.rb.linearVelocityY < 0 && !mPlayer.onWall && mPlayer.canChangeState)
        {
            this.mStateMachine.ChangeState(mPlayer.fall);
            return;
        }
    }
    private void MovingInAir()
    {
        if(mPlayer.g_moveInput.x != 0)
            mPlayer.SetVelocity
            (mPlayer.airMoveSpeed * mPlayer.g_moveInput.x, mPlayer.rb.linearVelocityY);
    }
}