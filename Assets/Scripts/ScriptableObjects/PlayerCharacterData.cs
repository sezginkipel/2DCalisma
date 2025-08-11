using UnityEngine;


[CreateAssetMenu(fileName = "PlayerCharacterData", menuName = "Scriptable Objects/Player Character Data")]

public class PlayerCharacterData : ScriptableObject
{
[Header("Genel Bilgiler")]
    public string characterName;
    [TextArea]
    public string description;
    public Sprite characterIcon;

    [Header("Temel İstatistikler")]
    public int maxHealth = 100;
    public float moveSpeed = 1f;
    public float attackSpeedMultiplier = 1f;
    public float meleeDamageMultiplier = 1f;
    public float rangedDamageMultiplier = 1f;
    public int armor = 0;
    public float critChance = 0f;

    [Header("Özel Yetenekler")]
    public bool hasPassiveAbility = false;
    public string passiveAbilityDescription;

    [Header("Kısıtlamalar")]
    public bool canUseMeleeWeapons = true;
    public bool canUseRangedWeapons = true;
    public bool usesOnlySpecialWeapons = false;

    [Header("Başlangıç Ekipmanları")]
    public GameObject[] startingWeapons;
    public GameObject[] startingItems;


}




