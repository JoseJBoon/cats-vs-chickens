using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public delegate void OnHealthHandler();
    
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject healthBarContainer;
    [field: SerializeField] public float CurrentHealth { get; private set; } = 100.0f;
    [field: FormerlySerializedAs("<maxHealth>k__BackingField")] [field: SerializeField] public float MaxHealth { get; private set; } = 100.0f;

    public event OnHealthHandler OnHealthDepleted;

    private void Awake()
    {
        HideHealthBar();
    }

    private void Start()
    {
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        healthBar.fillAmount = CurrentHealth / MaxHealth;
    }
    
    public void Heal(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        UpdateGUI();
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth == 0.0f)
            return;
        
        CurrentHealth -= amount;
        if (CurrentHealth < 1)
        {
            CurrentHealth = 0.0f;
            OnHealthDepleted?.Invoke();
        }
        UpdateGUI();
    }

    public void HideHealthBar()
    {
        healthBarContainer.SetActive(false);
    }
    
    public void DisplayHealthBar()
    {
        healthBarContainer.SetActive(true);
    }
}
