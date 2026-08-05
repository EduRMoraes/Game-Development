using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Patrol")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float pointReachedDistance = 0.1f;

    [Header("Detection")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float chaseRange = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private EnemyState currentState;
    private Transform currentTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentState = EnemyState.Patrol;
        currentTarget = pointB;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:

                if (distanceToPlayer <= detectionRange)
                {
                    currentState = EnemyState.Chase;
                }
                else
                {
                    Patrol();
                }

                break;

            case EnemyState.Chase:

                if (distanceToPlayer > chaseRange)
                {
                    currentState = EnemyState.Patrol;
                }
                else
                {
                    ChasePlayer();
                }

                break;
        }
    }

    private void Patrol()
    {
        MoveTowards(currentTarget.position);

        if (Vector2.Distance(transform.position, currentTarget.position) <= pointReachedDistance)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }
    }

    private void ChasePlayer()
    {
        MoveTowards(player.position);
    }

    private void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        Flip(direction.x);
    }

    private void Flip(float direction)
    {
        if (direction > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Detection Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Chase Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Patrol Points
        if (pointA != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointA.position, 0.1f);
        }

        if (pointB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pointB.position, 0.1f);
        }

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
