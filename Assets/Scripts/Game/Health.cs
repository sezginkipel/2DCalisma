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

    }
}
