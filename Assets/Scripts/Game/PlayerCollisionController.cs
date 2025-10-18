using UnityEngine;

/// <summary>
/// Handles the player's interactions with collectible items.
/// </summary>
public class PlayerCollisionController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible collectible = collision.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect(gameObject); // Pass in the player GameObject as the collector
        }
    }
}
