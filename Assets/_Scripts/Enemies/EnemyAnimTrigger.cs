using UnityEngine;

public class EnemyAnimTrigger : MonoBehaviour
{
    private EnemyContext mEnemy;
    private EnemyDamageDealer enemyDamageDealer;
    void Awake()
    {
        mEnemy = GetComponentInParent<EnemyContext>();
        enemyDamageDealer = GetComponentInParent<EnemyDamageDealer>();
    }

    public void AttackTrigger()
    {
        mEnemy.attackTrigger = true;
        enemyDamageDealer.DealDamage();
        Debug.Log("attack");
    }


    public void AnimationTrigger()
    {
        mEnemy.animTrigger = true;
    }

    public void ResetTriggers()
    {
        mEnemy.animTrigger = false;
    }
}
