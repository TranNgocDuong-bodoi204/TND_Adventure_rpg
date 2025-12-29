using System;
using UnityEngine;

public class EntityState
{
    protected StateMachine mStateMachine;
    protected string mAnimParameter;
    protected Animator mAnimator;
    private StateMachine stateMachine;
    private string animationParam;
    public virtual Type GroupType => GetType();

    public EntityState(StateMachine stateMachine, string animationParam)
    {
        this.mStateMachine = stateMachine;
        mAnimParameter = animationParam;
    }

    public virtual void EnterState()
    {
        mAnimator.SetBool(mAnimParameter, true);
    }

    public virtual void UpdateState()
    {
        
    }
    public virtual void FixUpdateState()
    {
        
    }
    public virtual void ExitState()
    {
        mAnimator.SetBool(mAnimParameter, false);
    }
    public virtual void OnTriggerEnterS(Collider2D collision)
    {
    }
    public virtual void OnTriggerExitS(Collider2D collision)
    {
    }
}
