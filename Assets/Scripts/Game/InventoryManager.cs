using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's collection of items and triggers stat recalculations.
/// </summary>
[RequireComponent(typeof(PlayerStats))]
public class InventoryManager : MonoBehaviour
{
    private readonly List<ItemData> _items = new List<ItemData>();
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    /// <summary>
    /// Adds a new item to the inventory and recalculates player stats.
    /// </summary>
    /// <param name="itemData">The item to add.</param>
    public void AddItem(ItemData itemData)
    {
        Debug.Log($"Item collected: {itemData.itemName}");
        _items.Add(itemData);
        
        // Notify PlayerStats to update with the new item bonuses
        _playerStats.RecalculateStats(_items);
    }
}
