using UnityEngine;

public class Player_AirJump : Player_Aired
{
    public Player_AirJump(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        mPlayer.didAirJump = false;
    }
    public override void ExitState()
    {
        base.ExitState();
        mPlayer.didJump = false;
    }
    public override void FixUpdateState()
    {
        DoAirJump();
        base.FixUpdateState();
    }
    private void DoAirJump()
    {
        if(!mPlayer.didAirJump)
        {
            mPlayer.rb.linearVelocity = Vector2.zero;
            mPlayer.rb.linearVelocityY = mPlayer.jumpForce;
            mPlayer.didAirJump = true;
        }
    }
}
