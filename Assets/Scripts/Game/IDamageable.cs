/// <summary>
/// Defines an entity that can take damage.
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Causes the entity to take a specified amount of damage.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to take.</param>
    void TakeDamage(float damageAmount);
}
