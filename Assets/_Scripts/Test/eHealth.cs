using Unity.VisualScripting;
using UnityEngine;

public class eHealth : MonoBehaviour, IHealth
{
    [SerializeField]private float maxHealh;
    [SerializeField]private float currentHealth;
    [SerializeField] private Rigidbody2D rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        SetCurrentHealth(maxHealh);
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
        PushBackWhenTakeDamage(damageInfo.dmg_hitDirection);

        if(GetCurrentHealth() == 0)
        {
            ObjectIsDead();
        }
    }
    public void PushBackWhenTakeDamage(Vector2 direction)
    {
        direction.Normalize();
        rb.AddForce(new Vector2(-direction.x * 3, direction.y * 10),ForceMode2D.Impulse);
    }
    private void ObjectIsDead()
    {
        Debug.Log("object dead");
    }
}
