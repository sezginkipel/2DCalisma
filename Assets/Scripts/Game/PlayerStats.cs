using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Player'ın tüm anlık istatistiklerini yöneten merkezi betik.
// Karakter verileri, eşyalar ve diğer güçlendirmelerden gelen tüm bonuslar burada toplanır ve hesaplanır.
public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [Tooltip("Oyuncu doğduğunda kaç saniye hasar almaz olacağını belirtir.")]
    public float spawnInvincibilityDuration = 3f;

    [Header("Data")]
    public PlayerCharacterData characterData;

    // Anlık (hesaplanmış) istatistikler
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MoveSpeed { get; private set; }
    public float AttackSpeedMultiplier { get; private set; }
    public float DamageMultiplier { get; private set; }
    public int Armor { get; private set; }
    public int Coin { get; private set; }

    // Durum
    private bool _isInvincible = false;

    private void Awake()
    {
        if (characterData == null)
        {
            Debug.LogError("PlayerStats için CharacterData atanmamış!");
            enabled = false;
            return;
        }
        InitializeStats();
    }

    private void Start()
    {
        StartCoroutine(SpawnProtectionCoroutine());
    }

    private IEnumerator SpawnProtectionCoroutine()
    {
        Debug.Log("Spawn koruması AÇIK.");
        _isInvincible = true;
        yield return new WaitForSeconds(spawnInvincibilityDuration);
        _isInvincible = false;
        Debug.Log("Spawn koruması KAPALI.");
    }

    // İstatistikleri başlangıç karakter verisine göre ayarlar.
    private void InitializeStats()
    {
        // Başlangıçta, envanter boşken istatistikleri hesapla
        RecalculateStats(new List<ItemData>());
    }

    /// <summary>
    /// Envanterdeki tüm eşyaları hesaba katarak oyuncunun istatistiklerini yeniden hesaplar.
    /// </summary>
    public void RecalculateStats(List<ItemData> items)
    {
        Debug.Log("Recalculating stats...");

        // 1. İstatistikleri temel karakter değerlerine sıfırla
        float oldMaxHealth = MaxHealth;
        MaxHealth = characterData.maxHealth;
        MoveSpeed = characterData.moveSpeed;
        AttackSpeedMultiplier = characterData.attackSpeedMultiplier;
        DamageMultiplier = characterData.meleeDamageMultiplier; // TODO: Silah tipine göre bunu daha dinamik yap
        Armor = characterData.armor;

        // 2. Tüm eşya bonuslarını uygula
        foreach (ItemData item in items)
        {
            MaxHealth += item.healthBonus;
            MoveSpeed += item.speedBonus;
            AttackSpeedMultiplier += item.attackSpeedBonus;
            DamageMultiplier += item.rangedDamageBonus; // TODO: ranged/melee ayrımı yap
            Armor += (int)item.armorBonus;
        }

        // 3. Canı ayarla
        // Maksimum can arttıysa, artış miktarı kadar can ekle
        float healthDifference = MaxHealth - oldMaxHealth;
        if (healthDifference > 0)
        {
            CurrentHealth += healthDifference;
        }
        // Canın, maksimum canı geçmediğinden emin ol
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        Debug.Log($"Stats Recalculated: HP={CurrentHealth}/{MaxHealth}, Speed={MoveSpeed}, AtkSpd={AttackSpeedMultiplier}");
    }

    public void TakeDamage(float damage)
    {
        if (_isInvincible) return; // Ölümsüzken hasar alma

        // Zırh hesaplaması (basit bir eksiltme)
        float damageAfterArmor = damage - Armor;
        if (damageAfterArmor < 1) damageAfterArmor = 1; // Minimum 1 hasar al

        CurrentHealth -= damageAfterArmor;
        Debug.Log($"Player took {damageAfterArmor} damage, {CurrentHealth}/{MaxHealth} HP left.");

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
        }
    }

    public void AddCoin(int amount)
    {
        Coin += amount;
        // TODO: Update UI Text for coins here via a UIManager event or direct call
        Debug.Log($"{amount} coin(s) added. Total coins: {Coin}");
    }

    // TODO: Eşya bonuslarını istatistiklere ekleyecek/çıkaracak metodlar eklenecek.
    // Örnek: public void AddStatBonus(ItemData item) { ... }
    // Örnek: public void RecalculateStats() { ... }
}
