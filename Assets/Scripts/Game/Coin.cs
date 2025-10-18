using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, ICollectible
{
    public int coinValue = 10;

    // Optional: Add sound effects or particle effects for collection
    // public GameObject collectionEffect;
    // public AudioClip collectionSound;

    public void Collect(GameObject collector)
    {
        PlayerStats playerStats = collector.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddCoin(coinValue);
            // Optional: Play effects at the coin's position
            // if (collectionEffect != null) Instantiate(collectionEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
