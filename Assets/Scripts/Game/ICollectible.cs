using UnityEngine;

/// <summary>
/// Defines an entity that can be collected by another GameObject.
/// </summary>
public interface ICollectible
{
    /// <summary>
    /// Logic to execute when the object is collected.
    /// </summary>
    /// <param name="collector">The GameObject that is collecting this object.</param>
    void Collect(GameObject collector);
}
