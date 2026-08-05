using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private float attackCooldown = 0.4f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack Point")]
    [SerializeField] private Transform attackPoint;

    private float lastAttackTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        Attack();
    }

    private void Attack()
{
    Debug.Log("⚔️ Ataque realizado.");

    Collider2D[] enemies = Physics2D.OverlapCircleAll(
        attackPoint.position,
        attackRange,
        enemyLayer
    );

    Debug.Log($"🎯 Objetos encontrados: {enemies.Length}");

    foreach (Collider2D enemy in enemies)
    {
        Debug.Log($"👀 Encontrou: {enemy.name}");

        Health health = enemy.GetComponent<Health>();

        if (health != null)
        {
            Debug.Log($"💥 Acertou {enemy.name}!");

            health.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogWarning($"⚠️ {enemy.name} não possui Health.");
        }
    }
}

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
