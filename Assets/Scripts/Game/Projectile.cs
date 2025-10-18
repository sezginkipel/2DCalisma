using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Stats
    private float _speed;
    private float _damage;

    // Lifetime
    public float lifetime = 2f;
    private float _lifetimeTimer;

    // Movement
    private Vector2 _direction;

    /// <summary>
    /// Initializes the projectile with its core stats right after instantiation.
    /// </summary>
    public void Initialize(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }

    void Start()
    {
        _lifetimeTimer = lifetime;
        // Destroy the projectile if it hasn't been initialized properly
        if (_speed == 0) {
            Debug.LogWarning("Projectile spawned without being initialized. Destroying.");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Merminin hareketi
        transform.Translate((_direction * _speed) * Time.deltaTime);

        // Yaşam süresi kontrolü
        _lifetimeTimer -= Time.deltaTime;
        if (_lifetimeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we hit can take damage
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Avoid damaging the player with their own projectile
            if (other.CompareTag("Player")) return;

            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
