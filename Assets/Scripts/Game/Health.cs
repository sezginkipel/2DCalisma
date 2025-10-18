using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) // TakeDamage -> float damage
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
        }
        // Optional: Destroy the player object or handle other death-related logic here
        // For example, you might want to disable player controls but leave the object for an animation
        // Destroy(gameObject);
    }
}
