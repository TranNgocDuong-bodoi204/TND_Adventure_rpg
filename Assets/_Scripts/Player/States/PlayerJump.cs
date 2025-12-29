using System.Collections;
using UnityEngine;

public class PlayerJump : Player_Aired
{
    public PlayerJump(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        mPlayer.didJump = false;
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixUpdateState()
    {
        DoJump();
        base.FixUpdateState();
    }
    
    private void DoJump()
    {
        if(!mPlayer.didJump)
        {
            mPlayer.rb.linearVelocity = Vector2.zero;
            mPlayer.rb.linearVelocityY = mPlayer.jumpForce;
            mPlayer.didJump = true;
            mPlayer.g_isJumping = true;
        }
    }

}
