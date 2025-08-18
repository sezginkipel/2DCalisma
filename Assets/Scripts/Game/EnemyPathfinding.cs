using UnityEngine;


public class EnemyPathfinding : MonoBehaviour
{
    [Header("Player")]
    private GameObject player;

    [Header("Particle System")]
    private ParticleSystem particleSystem;

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float playerDetectionRadius = 5f;

    [Header("Gizmos")]
    [SerializeField] private bool drawGizmos = true;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        particleSystem = GetComponent<ParticleSystem>();

        if (player == null)
        {
            Debug.LogError("Player sahnede bulunamadı! Player nesnesine Player tagi ekleyin.");
            Destroy(gameObject);
        }
    }



    void Update()
    {
        FollowPlayer();
        TryAttackPlayer();

    }

    void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

    void TryAttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
