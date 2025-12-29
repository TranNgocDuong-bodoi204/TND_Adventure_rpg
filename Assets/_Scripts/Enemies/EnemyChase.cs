using System;
using UnityEngine;

public class EnemyChase : EnemyState
{
    private bool lostDetection = false;
    private float lostDetectionTime = 0.5f; // seconds to wait before switching to idle
    private float lostDetectionStart = 0f;
    public EnemyChase(StateMachine stateMachine, string animationParam, EnemyContext context) : base(stateMachine, animationParam, context)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter Chase State");
        lostDetection = false;
        lostDetectionStart = 0f;
    }
    public override void ExitState()
    {
        base.ExitState();
        enemy.SetVelocity(0, enemy.rb.linearVelocityY);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        ChangeToIdle();
        ChangeToAttack();
    }
    public override void FixUpdateState()
    {
        base.FixUpdateState();
        ChasingPlayer();
    }

    private void ChangeToIdle()
    {
        //TH1: khi mất định vị người chơi -> idle
        //TH2: khi cần chạy đến vị trí người chơi, và chưa thể tấn công -> idle
        bool checkTimer = Time.time >= lostDetectionStart +lostDetectionTime;
        if(lostDetection 
           && checkTimer
           && !enemy.isPlayerDetected)
        {
            mStateMachine.ChangeState(enemy.idle);
            return;
        }
        if(checkTimer
           && enemy.PlayerInAttackRange
           && enemy.isInBattle)
        {
            mStateMachine.ChangeState(enemy.idle);
            return;
        }
    }

    private void ChangeToAttack()
    {
        if(enemy.PlayerInAttackRange && enemy.isCanAttack && enemy.timer <= 0)
        {
            mStateMachine.ChangeState(enemy.attack);
            return;
        }
    }


    private void ChasingPlayer()
    {
        RaycastHit2D hit = DetectPlayer();
        Vector2 direction = GetDirectionToPlayer();

        if(hit.collider != null && hit.collider.CompareTag(enemy.TAG_PLAYER))
        {
            enemy.isPlayerDetected = true;
            DoChasing(direction);
        }
        else
        {
            if(lostDetection == false)
            {
                lostDetection = true;
                lostDetectionStart = Time.time;
                Debug.Log("Lost Player");
            }
            enemy.moveDirection = Vector2.zero;
            enemy.SetVelocity(0, enemy.rb.linearVelocityY);
        }
    }
    private RaycastHit2D DetectPlayer()
    {
        if(enemy.playerReference == null) return new RaycastHit2D();

        Vector2 direction = GetDirectionToPlayer();
        float distance = GetDistanceToPlayer();

        Debug.DrawLine(enemy.eyePosition.position,enemy.eyePosition.position + (Vector3)(direction * distance),Color.red);

        return Physics2D.Raycast
                            (enemy.eyePosition.position,
                             direction,
                             distance,
                             ~enemy.ignoreLayer);
    }

    private void DoChasing(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(Vector2.right, direction);
            if(angle >= 80 && angle <= 100)
            {
                enemy.moveDirection = Vector2.zero;
                enemy.SetVelocity(0, enemy.rb.linearVelocityY);
            }
            else
            {    
                lostDetection = false;
                enemy.moveDirection = direction;
                enemy.SetVelocity(enemy.moveDirection.x * enemy.g_ChaseSpeed, enemy.rb.linearVelocityY);
                Debug.Log("Chasing Player");
            }
    }
}
