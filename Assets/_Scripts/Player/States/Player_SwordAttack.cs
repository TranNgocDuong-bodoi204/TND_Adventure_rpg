using UnityEngine;

public class Player_SwordAttack : PlayerState
{
    private int attackCount = 2;

    public Player_SwordAttack(
        StateMachine stateMachine,
        string animationParam,
        PlayerContext player
    ) : base(stateMachine, animationParam, player)
    {
    }

    public override void EnterState()
    {
        mPlayer.attackBuffered = false;
        mPlayer.animTrigger = false;
        Debug.Log("enter sword attack");
        SetAttackIndex();
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (mPlayer.p_inputActions.Player.Attack.WasPressedThisFrame() && !mPlayer.animTrigger)
        {
            mPlayer.attackBuffered = true;
        }

        ChangeToIdle();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        AttackPush();
    }
    private void ChangeToIdle()
    {
        if(mPlayer.animTrigger)
        {
            mStateMachine.ChangeState(mPlayer.idle);
            return;
        }
    }
    private void ReChangeToAttack()
    {
        if(mPlayer.animTrigger && mPlayer.attackBuffered)
        {
            Debug.Log("rechange To Attack");
            mStateMachine.ChangeState(mPlayer.swordAttack, true);
            return;
        }
    }
    private void SetAttackIndex()
    {
        if(mPlayer.attackIndex >= attackCount - 1)
        {
            mPlayer.attackIndex = 0;
        }
        else
        {
            mPlayer.attackIndex++;
        }
    }
    private void AttackPush()
    {
        mPlayer.SetVelocity(
            mPlayer.facingDirection.x * mPlayer.attackPushForce,
            mPlayer.rb.linearVelocity.y
        );
    }
}
