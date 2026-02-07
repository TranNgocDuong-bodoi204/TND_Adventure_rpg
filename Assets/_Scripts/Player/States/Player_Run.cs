using UnityEngine;

public class Player_Run : Player_Grounded
{
    public Player_Run(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Player Enter run");
    }
    public override void ExitState()
    {
        base.ExitState();
        mPlayer.rb.linearVelocity = Vector2.zero;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToIdle();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        Moving();
    }
    
    private void Moving()
    {
        mPlayer.SetVelocity(mPlayer.g_moveInput.x * mPlayer.g_moveSpeed, mPlayer.rb.linearVelocityY);
    }

    private void ChangeToIdle()
    {
        if(mPlayer.g_moveInput.x == 0)
        {
            mStateMachine.ChangeState(mPlayer.idle);
        }
    }
}
