using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Düşman öldüğünde yapılacak işlemler (örneğin, puan ekleme, efekt oynatma vb.)
        Destroy(gameObject);
    }

    
}
