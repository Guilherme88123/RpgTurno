using Domain.Dto.Global;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Base.HealthBar;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;

namespace Domain.Model.Entity.Units.Base;

public class BaseUnitEntity : BaseEntity
{
    public string Name { get; set; }

    public int MaxHealth { get; set; } = 10;
    public int Health { get; set; }

    public int Damage { get; set; } = 4;

    public bool IsRanged { get; protected set; }

    public SpriteData Icon { get; protected set; }

    private readonly HealthBarComponent _healthBar;

    private const float DelayDamageTakenFlash = 0.1f;
    private float _currentDelayDamageTakenFlash;
    private bool HasTakeDamage => _currentDelayDamageTakenFlash > 0;

    public BaseUnitEntity()
    {
        Health = MaxHealth; 
        
        _healthBar = new HealthBarComponent(MaxHealth, Health);
    }

    public override void Update()
    {
        base.Update();

        UpdateDelays();
        UpdateHealthBarComponent();
        UpdateTakeDamageEffect();
    }

    private void UpdateDelays()
    {
        _currentDelayDamageTakenFlash = Math.Max(0, _currentDelayDamageTakenFlash - GlobalVariablesDto.DeltaTime);
    }

    private void UpdateHealthBarComponent()
    {
        _healthBar.SetPosition((int)PositionX + SizeX / 2 - _healthBar.Bounds.Width / 2, (int)PositionY + SizeY);
        _healthBar.SetValues(MaxHealth, Health);
        _healthBar.Update(GlobalVariablesDto.GameTime);
    }

    private void UpdateTakeDamageEffect()
    {
        if (HasTakeDamage)
            Color = Color.Red;
        else 
            Color = Color.White;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;

        if (Health < 0)
            Destroy();

        ResetTakeDamageDelay();
    }

    private void ResetTakeDamageDelay()
    {
        _currentDelayDamageTakenFlash = DelayDamageTakenFlash;
    }

    public override void Draw()
    {
        base.Draw();


        DrawHealthBar();
    }

    protected virtual void DrawHealthBar()
    {
        _healthBar.Draw(GlobalVariablesDto.SpriteBatchEntities);
    }
}
