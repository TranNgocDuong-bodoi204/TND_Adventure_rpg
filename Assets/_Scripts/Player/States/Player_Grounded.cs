using UnityEngine;

public class Player_Grounded : PlayerState
{
    public Player_Grounded(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
         mPlayer.canChangeState = true;
        mPlayer.didJump = false;
        mAnimator.SetFloat(this.mPlayer.MOVE_SPEED_PARAM, Mathf.Abs(mPlayer.g_moveInput.x));
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToJump();
        ChangeToAttack();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        
        ChangeToFall();
    }

    protected virtual void ChangeToAttack()
    {
        if(mPlayer.p_inputActions.Player.Attack.WasPressedThisFrame() && mPlayer.animTrigger)
        {
            mStateMachine.ChangeState(mPlayer.swordAttack);
            return; 
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

    private void ChangeToJump()
    {
        if(mPlayer.onGround)
        {
            if(mPlayer.p_inputActions.Player.Jump.WasPressedThisFrame() ||
               mPlayer.p_inputActions.PlayerMobile.Jump.WasPressedThisFrame() )
                mStateMachine.ChangeState(mPlayer.jump);
            else if(mPlayer.g_moveInput.y > 0)
                mStateMachine.ChangeState(mPlayer.jump);
        }
    }
    
}
