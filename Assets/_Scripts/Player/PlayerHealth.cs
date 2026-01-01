using System;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    // observer pattern
    public static event Action<float,float,float> onPlayerInitalizeHealth;// min - max - current
    public static event Action<float> onPlayerHealthChanged;
    // dùng cho HUB PlayerHealthBarCrl
    protected override void InitialHealth()
    {
        base.InitialHealth();
        onPlayerInitalizeHealth?.Invoke(0,maxHealh,currentHealth);
    }
    protected override void WhenHealthChanged()
    {
        base.WhenHealthChanged();
        onPlayerHealthChanged?.Invoke(currentHealth);
    }
}
