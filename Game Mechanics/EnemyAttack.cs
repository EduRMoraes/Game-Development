using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1f;

    private float cooldownTimer;

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void TryAttack()
    {
        Debug.Log("[EnemyAttack] Tentando atacar...");

        if (cooldownTimer > 0f)
        {
            Debug.Log("[EnemyAttack] Ataque em cooldown.");
            return;
        }

        cooldownTimer = attackCooldown;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            playerLayer
        );

        Debug.Log($"[EnemyAttack] Jogadores encontrados: {hits.Length}");

        if (hits.Length == 0)
        {
            Debug.Log("[EnemyAttack] Nenhum jogador dentro do alcance.");
        }

        foreach (Collider2D hit in hits)
        {
            Debug.Log($"[EnemyAttack] Acertou: {hit.name}");

            Health health = hit.GetComponent<Health>();

            if (health != null)
            {
                Debug.Log($"[EnemyAttack] Aplicando {damage} de dano.");

                health.TakeDamage(damage);
            }
            else
            {
                Debug.Log($"[EnemyAttack] {hit.name} não possui Health.");
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
