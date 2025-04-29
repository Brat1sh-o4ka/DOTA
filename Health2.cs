public class Player : Area2D
{
    private Health _health;

    public override void _Ready()
    {
        _health = GetNode<Health>("Health");
        _health.Connect(nameof(Health.HealthDepleted), this, nameof(OnDeath));
        _health.Connect(nameof(Health.HealthChanged), this, nameof(OnHealthChanged));
    }

    private void OnDeath()
    {
        GD.Print("Игрок умер!");
        // Логика смерти персонажа
    }

    private void OnHealthChanged(float current, float max)
    {
        GD.Print($"Здоровье изменилось: {current}/{max}");
        // Обновление UI здоровья и т.д.
    }

    public void OnHit(float damage)
    {
        _health.TakeDamage(damage);
    }
}
