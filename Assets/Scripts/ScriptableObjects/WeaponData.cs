using UnityEngine;



public enum WeaponType { Melee, Ranged, Special }



[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]

public class WeaponData : ScriptableObject

{

  public string weaponName;

  [TextArea]

  public string description;

  public Sprite icon;

  public WeaponType weaponType;



  [Header("Savaş Değerleri")]

  public float damage;

  public float attackCooldown; // saniye cinsinden

  public float range;

  public float projectileSpeed;

  public float knockback;

  public int pierceCount;



  [Header("Özel Efektler")]

  public bool hasOnHitEffect = false;

  public string onHitEffectDescription;



  public GameObject weaponPrefab;

}