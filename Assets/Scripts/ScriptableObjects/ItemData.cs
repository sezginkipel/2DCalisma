using UnityEngine;



public enum ItemRarity { Common, Rare, Epic, Legendary }



[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ItemData")]

public class ItemData : ScriptableObject

{

  public string itemName;

  [TextArea]

  public string description;

  public Sprite icon;

  public ItemRarity rarity;



  [Header("Stat Artışları")]

  public int healthBonus;

  public float speedBonus;

  public float meleeDamageBonus;

  public float rangedDamageBonus;

  public float attackSpeedBonus;

  public float armorBonus;

  public float critChanceBonus;



  [Header("Özel Etkiler")]

  public bool hasSpecialEffect = false;

  public string specialEffectDescription;



  public GameObject itemPrefab;

}