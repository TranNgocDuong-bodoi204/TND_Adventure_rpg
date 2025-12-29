using System.Linq;
using UnityEditor;
using UnityEngine;

public class eDamge : MonoBehaviour, IDamageDealer
{
    [Header("Set up damage")]
    [SerializeField] private float damage;
    [Header("Set up casting damage dealer ")]
    [SerializeField] private Transform damagePoint;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask whatIsTarget;
    // khi cast collide sẽ gọi health để takeDamage.
    // nhiệm vụ của damage check collide object chưa health
    void Update()
    {
        DealDamage();
    }
    public void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(damagePoint.position,damageRadius, whatIsTarget);
        if(colliders.Count() != 0)
        {
            foreach(var target in colliders)
            {
                IHealth health = target.gameObject.GetComponent<IHealth>();
                if(health != null)
                {
                    DamageInfo damageInfo = new DamageInfo
                    {
                    dmg_damageAmount =   damage,
                    dmg_damageDealer = this.gameObject,
                    dmg_hitDirection = transform.right
                    };
                    health.TakeDamage(damageInfo);
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        if(damagePoint == null) return;
        Gizmos.DrawSphere(damagePoint.position,damageRadius);
    }
}
