using UnityEngine;

/// <summary>
/// Manages the state and firing logic for a single weapon instance.
/// This component is controlled by a PlayerWeaponController.
/// </summary>
public class WeaponController : MonoBehaviour
{
    // References
    private WeaponData _weaponData;
    private PlayerStats _playerStats;
    private Transform _firePoint;
    private AudioSource _weaponAudioSource;

    // State
    private float _fireCooldown;

    /// <summary>
    /// Configures the weapon with its data and necessary player references.
    /// </summary>
    public void Initialize(WeaponData data, PlayerStats stats, Transform firePoint, AudioSource audioSource)
    {
        _weaponData = data;
        _playerStats = stats;
        _firePoint = firePoint;
        _weaponAudioSource = audioSource;
        _fireCooldown = 0f; // Start ready to fire
    }

    /// <summary>
    /// Updates the cooldown. This should be called every frame by the owner.
    /// </summary>
    public void UpdateCooldown()
    {
        if (_fireCooldown > 0)
        {
            _fireCooldown -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Attempts to fire the weapon at the given target if the cooldown is ready.
    /// </summary>
    public void TryFire(Transform target)
    {
        if (_weaponData == null) return; // Not initialized
        if (target == null) return; // No target
        if (_fireCooldown > 0) return; // On cooldown

        Fire(target);
    }

    private void Fire(Transform target)
    {
        // Reset cooldown based on stats
        _fireCooldown = _weaponData.attackCooldown / _playerStats.AttackSpeedMultiplier;

        // Play sound
        if (_weaponAudioSource != null && _weaponData.fireSound != null)
        {
            _weaponAudioSource.PlayOneShot(_weaponData.fireSound);
        }

        // Create and initialize projectile
        GameObject projectileGO = Instantiate(_weaponData.projectilePrefab, _firePoint.position, Quaternion.identity);
        Projectile projectileScript = projectileGO.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            // Calculate final stats
            float finalDamage = _weaponData.damage * _playerStats.DamageMultiplier;
            float finalSpeed = _weaponData.projectileSpeed; // Can also be multiplied by a player stat if needed

            // Initialize and fire projectile
            projectileScript.Initialize(finalDamage, finalSpeed);
            Vector2 direction = (target.position - _firePoint.position).normalized;
            projectileScript.SetDirection(direction);
        }
    }
}

