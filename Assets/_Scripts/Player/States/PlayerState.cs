using System.Collections;
using UnityEngine;

public class PlayerState : EntityState
{
    protected PlayerContext mPlayer;
    protected bool attackStateIsOver = false;

    public PlayerState(StateMachine stateMachine, string animationParam,PlayerContext player) : base(stateMachine, animationParam)
    {
        mPlayer = player;
        this.mAnimator = player.animator;
    }

    public override void EnterState()
    {
        base.EnterState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToDash();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
    }

    protected void SetCoolDown(float cooldown)
    {
        mPlayer.StartCoroutine(dashCooldownTimer(cooldown));
    }
    protected virtual void ChangeToDash()
    {
        if(mPlayer.g_isDashPressed && mPlayer.canDash && !mPlayer.onWall)
        {
            mStateMachine.ChangeState(mPlayer.dash);
            return;
        }
    }
    private IEnumerator dashCooldownTimer(float cooldown)
    {
        mPlayer.canDash = false;
        yield return new WaitForSeconds(cooldown);
        mPlayer.canDash = true;
    }
}
