using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public GameObject coinPrefab;

    public void TakeDamage(float damage)
    {
        health -= damage;
        // Take damage effect
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            Invoke("ResetColor", 0.1f);
        }
        if (health <= 0f)
        {
            DropCoin();
            Die();
        }
    }

    void Die()
    {
        // Düşman öldüğünde yapılacak işlemler (örneğin, puan ekleme, efekt oynatma vb.)
        Destroy(gameObject);
    }

    void ResetColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.white;
        }
    }


    void DropCoin()
    {
        int coinCount = Random.Range(1, 4); // 1 ile 3 arasında rastgele bir sayı
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
