using UnityEngine;

public class Player_Fall : Player_Aired
{
    public Player_Fall(StateMachine stateMachine, string animationParam, PlayerContext p) : base(stateMachine, animationParam, p)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToIdle();
    }
    private void ChangeToIdle()
    {
        if(mPlayer.onGround)
        {
            this.mStateMachine.ChangeState(mPlayer.idle);
            return;
        }
    }
}
