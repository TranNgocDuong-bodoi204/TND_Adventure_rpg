using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour,IHealth
{
    

    [SerializeField]protected float maxHealh;
    [SerializeField]protected float currentHealth;
    [SerializeField] private Rigidbody2D rb;
    public Vector2 pushBackForceWhenHit = new Vector2(4,3);

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        SetCurrentHealth(maxHealh);
        InitialHealth();
    }
    public float GetCurrentHealth() => currentHealth;

    public float GetMaxHealth() => maxHealh;
    public void SetCurrentHealth(float value)
    {
        if(value <= 0)
            currentHealth = 0;
        else
            currentHealth = value;
    }

    public void TakeDamage(DamageInfo damageInfo) // dmg
    {

        SetCurrentHealth(GetCurrentHealth() - damageInfo.dmg_damageAmount);
        PushBackWhenTakeDamage(damageInfo.dmg_hitDirection, pushBackForceWhenHit);

        WhenHealthChanged();

        if(GetCurrentHealth() == 0)
        {
            ObjectIsDead();
        }
    }
    public void PushBackWhenTakeDamage(Vector2 direction, Vector2 force)
    {
        direction.Normalize();
        Vector2 pushForce = new Vector2(direction.x * force.x, force.y);
        rb.linearVelocity = pushForce;
    }
    private void ObjectIsDead()
    {
        Debug.Log("object dead");
    }

    // object con override
    protected virtual void InitialHealth()
    {
        
    }
    protected virtual void WhenHealthChanged()
    {
    }
}
