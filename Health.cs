using Godot;

public class Health : Node
{
    [Signal]
    public delegate void HealthChanged(float currentHealth, float maxHealth);
    [Signal]
    public delegate void HealthDepleted();
    [Signal]
    public delegate void HealthIncreased(float amount);
    [Signal]
    public delegate void HealthDecreased(float amount);

    [Export]
    public float MaxHealth = 100f;
    
    private float _currentHealth;

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            float previousHealth = _currentHealth;
            _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
            
            EmitSignal(nameof(HealthChanged), _currentHealth, MaxHealth);
            
            if (previousHealth < _currentHealth)
            {
                EmitSignal(nameof(HealthIncreased), _currentHealth - previousHealth);
            }
            else if (previousHealth > _currentHealth)
            {
                EmitSignal(nameof(HealthDecreased), previousHealth - _currentHealth);
            }
            
            if (_currentHealth <= 0f)
            {
                EmitSignal(nameof(HealthDepleted));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f) return;
        CurrentHealth -= damage;
    }

    public void Heal(float amount)
    {
        if (amount <= 0f) return;
        CurrentHealth += amount;
    }

    public void SetHealth(float newHealth)
    {
        CurrentHealth = newHealth;
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public bool IsAlive()
    {
        return _currentHealth > 0f;
    }

    public float GetHealthPercentage()
    {
        return _currentHealth / MaxHealth;
    }
}
