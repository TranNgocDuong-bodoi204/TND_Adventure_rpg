using UnityEngine;

public class Player_Idle : Player_Grounded
{
    public Player_Idle(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        mPlayer.SetVelocity(0, mPlayer.rb.linearVelocityY);
        base.EnterState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToRun();
    }

    private void ChangeToRun()
    {
        if(mPlayer.g_moveInput.x != 0)
        {
            mStateMachine.ChangeState(mPlayer.run);
        }
    }
}
