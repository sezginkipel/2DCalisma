using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioSource weaponAudioSource;
    public AudioClip fireSound;
    //public AudioClip reloadSound;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float weaponRange = 100f;
    public float fireRate = 0.5f;
    private float _fireCooldown;
    private Transform _target;


    void Update()
    {
        FindClosestEnemy();

        _fireCooldown -= Time.deltaTime;
        if (_fireCooldown <= 0f && _target != null)
        {
            FireProjectile();
            _fireCooldown = fireRate;
            if (weaponAudioSource != null && fireSound != null)
            {
                weaponAudioSource.PlayOneShot(fireSound);
            }
        }
    }

    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }

        if (closestEnemy != null && distanceToClosestEnemy <= weaponRange * weaponRange)
        {
            _target = closestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null && _target != null)
        {
            Vector2 direction = (_target.position - firePoint.position).normalized;
            projectileScript.SetDirection(direction);
        }
    }
}
