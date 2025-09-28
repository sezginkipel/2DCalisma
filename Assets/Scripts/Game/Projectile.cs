using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 200f;
    public float damage = 10f;
    public float lifetime = 2f;
    private Vector2 _direction;
    private float _lifetimeTimer;

    void Start()
    {
        _lifetimeTimer = lifetime;
    }

    void Update()
    {
        // Merminin hareketi
        transform.Translate((_direction * speed) * Time.deltaTime);

        // Yaşam süresi kontrolü
        _lifetimeTimer -= Time.deltaTime;
        if (_lifetimeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized; // Yönü normalize et, böylece hız sabit kalır.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer mermi bir düşmana çarparsa
        if (other.CompareTag("Enemy"))
        {
            // Düşmana hasar ver
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Mermiyi yok et
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Engel ile çarpışma durumunda mermiyi yok et
            Destroy(gameObject);
        }
    }
}
