using UnityEngine;

/// <summary>
/// Represents a collectible item in the game world. When collected, it adds its
/// associated ItemData to the player's inventory.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour, ICollectible
{
    [Tooltip("Bu objenin temsil ettiği eşya verisi.")]
    public ItemData itemData;

    public void Collect(GameObject collector)
    {
        if (itemData == null)
        {
            Debug.LogWarning("ItemPickup has no ItemData assigned.", this);
            return;
        }

        InventoryManager inventory = collector.GetComponent<InventoryManager>();
        if (inventory != null)
        {
            inventory.AddItem(itemData);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning($"{collector.name} tried to collect an item but has no InventoryManager.");
        }
    }

    // Optional: Add a visual representation of the item
    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && itemData != null)
        {
            sr.sprite = itemData.icon;
        }
    }
}
