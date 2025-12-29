using UnityEngine;

public class AttackDetecedCollider : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private EnemyContext enemy;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        enemy = GetComponentInParent<EnemyContext>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(enemy.TAG_PLAYER))
        {
            enemy.OnPlayerInAttackRange();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(enemy.TAG_PLAYER))
        {
            enemy.OnPlayerOutAttackRange();
        }
    }
}
