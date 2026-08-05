using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;

        Debug.Log($"[Health] {gameObject.name} nasceu com {CurrentHealth}/{maxHealth} HP.");

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        CurrentHealth -= damage;

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        Debug.Log($"[Health] {gameObject.name} recebeu {damage} de dano.");
        Debug.Log($"[Health] Vida restante: {CurrentHealth}/{maxHealth}");

        // Atualiza HUD, barras de vida etc.
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0)
            return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);

        Debug.Log($"[Health] {gameObject.name} curou {amount} HP.");
        Debug.Log($"[Health] Vida atual: {CurrentHealth}/{maxHealth}");

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log($"[Health] {gameObject.name} morreu.");

        OnDeath?.Invoke();

        // Temporário
        Destroy(gameObject);
    }
}
