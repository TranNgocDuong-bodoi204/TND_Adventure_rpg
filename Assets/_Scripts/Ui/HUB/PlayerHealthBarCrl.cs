using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarCrl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private float maxHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    void Awake()
    {
        slider = this.GetComponent<Slider>();
        healthText = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        PlayerHealth.onPlayerInitalizeHealth += InitalizeHealthBar;
        PlayerHealth.onPlayerHealthChanged += UpdateHealthBar;
    }
    void OnDisable()
    {
        PlayerHealth.onPlayerInitalizeHealth -= InitalizeHealthBar;
        PlayerHealth.onPlayerHealthChanged -= UpdateHealthBar;
    }

    private void InitalizeHealthBar(float min, float max, float value)
    {
        maxHealth = max;
        this.slider.minValue = min;
        this.slider.maxValue = maxHealth; 
        this.slider.value = value;
        SetHealthString(value,max);
        Debug.Log("InitalizeHealthBar");
    }
    private void UpdateHealthBar(float value)
    {
        this.slider.value = value;
        SetHealthString(value);
        Debug.Log("UpdateHealthBar");
    }

    private void SetHealthString(float max, float current)
    {
        string healthTxt = $"{current} / {max}";
        healthText.text =  healthTxt;
    }
    private void SetHealthString(float current)
    {
        healthText.text = $"{current} / {maxHealth}";
    }
    
}
