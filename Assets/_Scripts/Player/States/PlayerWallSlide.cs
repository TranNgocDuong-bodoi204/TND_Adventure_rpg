using UnityEngine;

public class PlayerWallSlide : PlayerState
{ 
    private float wallSlideSpeed = -.3f;
    private Transform animatorTransform; // transform của animator để xoay nhân vật khi wall slide
    public PlayerWallSlide(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        animatorTransform = mPlayer.animator.transform;
        RotateAnimator();
        base.EnterState();
    }
    public override void ExitState()
    {
        RotateAnimator();
        base.ExitState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToIdle();
        ChangeToWallJump();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        WallSliding();
    }
    private void WallSliding()
    {
        if(mPlayer.g_moveInput.y == -1) // khi ấn xuống
            mPlayer.SetVelocity(0, mPlayer.rb.linearVelocityY);
        else // trượt tường bình thường
        mPlayer.SetVelocity(0, wallSlideSpeed);
    }
    private void ChangeToIdle()
    {
        if(mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.idle);
            return;
        }
    }
    private void ChangeToWallJump()
    {
        if(mPlayer.p_inputActions.Player.Jump.WasPressedThisFrame() && mPlayer.onWall && !mPlayer.onGround ||
          mPlayer.p_inputActions.PlayerMobile.Jump.WasPressedThisFrame() && mPlayer.onWall && !mPlayer.onGround)
        {
            mAnimator.SetFloat(mPlayer.Y_VELOCITY_PARAM, 0.1f); // để play animation nhảy
            this.mStateMachine.ChangeState(mPlayer.wallJump);
            return;
        }
    }
    private void RotateAnimator()
    {
        animatorTransform.Rotate(0, 180, 0);
    }

}
