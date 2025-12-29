using System.Collections;
using UnityEngine;

public class Enemy_FallBack : EnemyState
{
    private float fallBackDuration = 0.5f;
    private float fallBackHorizontalSpeed = 6f;
    private float fallBackVerticalSpeed = 5f;
    private bool isFallBackComplete = false;
    public Enemy_FallBack(StateMachine stateMachine, string animationParam, EnemyContext context) : base(stateMachine, animationParam, context)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        isFallBackComplete = false;
        DoFallBack();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(isFallBackComplete)
        {
            ChangeToIdle();
        }
    }
    private void ChangeToIdle()
    {
        mStateMachine.ChangeState(enemy.idle);
        return;
    }

    private void DoFallBack()
    {
        enemy.StartCoroutine(FallBackCoroutine()); 
    }


    private IEnumerator FallBackCoroutine()
    {
        Vector2 fallbackVelocity = new Vector2(-enemy.facingDirection.x * fallBackHorizontalSpeed, fallBackVerticalSpeed);
        enemy.rb.linearVelocity = fallbackVelocity;
        Debug.Log("Fallback triggered! Velocity: " + fallbackVelocity);

        yield return new WaitForSeconds(fallBackDuration);
        enemy.SetVelocity(new Vector2(0f, enemy.rb.angularVelocity));
        isFallBackComplete = true;
        Debug.Log("Fallback ended! Velocity reset.");
    }
}
