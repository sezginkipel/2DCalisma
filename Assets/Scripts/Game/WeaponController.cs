using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioSource weaponAudioSource;
    public AudioClip fireSound;
    //public AudioClip reloadSound;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float weaponRange = 10f;
    public float fireRate = 0.5f;
    private float _fireCooldown;
    private Vector2 enemyPosition;


    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyPosition = collision.transform.position;
            Debug.Log("Düşman pozisyonu: " + enemyPosition);
        }
    }

    void Update()
    {
        _fireCooldown -= Time.deltaTime;
        if (_fireCooldown <= 0f)
        {
            // Düşmanın menzilde olup olmadığını kontrol et
            if (Vector2.Distance(transform.position, enemyPosition) <= weaponRange)
            {
                FireProjectile();
                _fireCooldown = fireRate;
                if (weaponAudioSource != null && fireSound != null)
                {
                    weaponAudioSource.PlayOneShot(fireSound);
                }
            }
        }
    }
    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            // Merminin yönünü belirle
            Vector2 direction = firePoint.up;
            projectileScript.SetDirection(enemyPosition);
        }
    }
}
