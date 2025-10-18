using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyData enemyData;
    private float _currentHealth;

    private void Start()
    {
        if (enemyData == null)
        {
            Debug.LogError("EnemyData is not assigned on " + gameObject.name);
            enabled = false;
            return;
        }

        _currentHealth = enemyData.maxHealth;

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RegisterEnemy(this);
        }
    }

    private void OnDestroy()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        
        // Optional: Add a damage flash effect
        StartCoroutine(DamageFlash());

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private System.Collections.IEnumerator DamageFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }

    void Die()
    {
        // TODO: Use enemyData.xpReward
        DropCoin();
        Destroy(gameObject);
    }

    void DropCoin()
    {
        // TODO: This should be handled by a LootManager system
        // For now, we just drop a fixed amount based on data
        int coinCount = enemyData.goldReward;
        // This part is problematic as it assumes a coin prefab is known.
        // This should be moved to a proper loot system later.
        // if (coinPrefab != null && coinCount > 0) {
        //     Instantiate(coinPrefab, transform.position, Quaternion.identity);
        // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to the player on contact
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            if (player != null)
            {
                player.TakeDamage(enemyData.damage);
            }
        }
    }
}

