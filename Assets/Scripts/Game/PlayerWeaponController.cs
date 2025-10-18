using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all weapons for the player. It initializes them based on character data
/// and orchestrates their firing logic in a centralized Update loop.
/// </summary>
[RequireComponent(typeof(PlayerStats))]
public class PlayerWeaponController : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Mermilerin çıkacağı ana nokta. Tüm silahlar bu noktayı kullanır.")]
    public Transform firePoint;
    [Tooltip("Tüm silahların seslerini çalmak için kullanacağı ortak AudioSource.")]
    public AudioSource weaponAudioSource;
    [Tooltip("Silahların düşmanları algılayacağı maksimum menzil.")]
    public float weaponRange = 15f;

    private PlayerStats _playerStats;
    private List<WeaponController> _weapons = new List<WeaponController>();
    private Transform _currentTarget;

    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();

        if (firePoint == null)
        {
            Debug.LogError("Fire Point atanmamış! Lütfen PlayerWeaponController üzerindeki alana atama yapın.", this);
            enabled = false;
            return;
        }

        InitializeStartingWeapons();
    }

    void InitializeStartingWeapons()
    {
        if (_playerStats.characterData == null) return;

        foreach (WeaponData weaponData in _playerStats.characterData.startingWeapons)
        {
            // Create a new GameObject for each weapon to keep things clean
            GameObject weaponGO = new GameObject(weaponData.weaponName);
            weaponGO.transform.SetParent(transform); // Attach to player
            weaponGO.transform.localPosition = Vector3.zero;

            WeaponController weaponCtrl = weaponGO.AddComponent<WeaponController>();
            weaponCtrl.Initialize(weaponData, _playerStats, firePoint, weaponAudioSource);
            _weapons.Add(weaponCtrl);
        }
    }

    void Update()
    {
        FindClosestEnemy();

        // Update and try to fire all weapons
        foreach (WeaponController weapon in _weapons)
        {
            weapon.UpdateCooldown();
            weapon.TryFire(_currentTarget);
        }
    }

    void FindClosestEnemy()
    {
        if (EnemyManager.Instance == null) 
        {
            _currentTarget = null;
            return;
        }

        float minDistanceSqr = weaponRange * weaponRange;
        Transform closestEnemy = null;

        foreach (Enemy enemy in EnemyManager.Instance.ActiveEnemies)
        {
            float distanceSqr = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                closestEnemy = enemy.transform;
            }
        }

        _currentTarget = closestEnemy;
    }
}
