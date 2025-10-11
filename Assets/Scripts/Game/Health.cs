using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _currentHealth;
    public GameObject deathPanel; // Ölüm paneli referansı

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
        if (deathPanel != null)
        {
            deathPanel.SetActive(true); // Ölüm panelini aktif et
        }
        Time.timeScale = 0f; // Oyunu durdur
    }
}
