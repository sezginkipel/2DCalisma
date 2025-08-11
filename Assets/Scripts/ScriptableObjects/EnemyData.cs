using UnityEngine;



[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/EnemyData")]

public class EnemyData : ScriptableObject

{

  public string enemyName;

  public Sprite icon;

  public GameObject prefab;



  [Header("İstatistikler")]

  public int maxHealth;

  public float moveSpeed;

  public float damage;

  public int armor;

  public float attackCooldown;



  [Header("Davranışlar")]

  public bool isRanged;

  public float detectionRange;



  [Header("Ödüller")]

  public int xpReward;

  public int goldReward;

}