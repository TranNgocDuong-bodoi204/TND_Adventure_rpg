using UnityEngine;

public interface IHealth
{
    public void TakeDamage(DamageInfo damageInfo);
    public float GetCurrentHealth();
    public float GetMaxHealth();
}
public class DamageInfo
{
    public float dmg_damageAmount;
    public Vector2 dmg_hitDirection;
    public GameObject dmg_damageDealer;
}
