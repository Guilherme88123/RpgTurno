using Domain.Dto.Global;
using Domain.Model.Entity.Base;
using Domain.Model.Entity.Units.Base.HealthBar;
using Domain.Model.Texture.Sprite;

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

    public BaseUnitEntity()
    {
        Health = MaxHealth; 
        
        _healthBar = new HealthBarComponent(MaxHealth, Health);
    }

    public override void Update()
    {
        base.Update();

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        UpdateHealthBarComponent();
    }

    private void UpdateHealthBarComponent()
    {
        _healthBar.SetPosition((int)PositionX + SizeX / 2 - _healthBar.Bounds.Width / 2, (int)PositionY + SizeY);
        _healthBar.SetValues(MaxHealth, Health);
        _healthBar.Update(GlobalVariablesDto.GameTime);
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
