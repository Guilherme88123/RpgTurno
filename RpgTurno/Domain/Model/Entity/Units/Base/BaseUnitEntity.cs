using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Components.Image;
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

        CreatureState = CreatureStateType.Idle;

        UpdateHealthBar(GlobalVariablesDto.GameTime);
    }

    private void UpdateHealthBar(GameTime gameTime)
    {
        _healthBar.SetPosition((int)PositionX, (int)PositionY + SizeY);
        _healthBar.Update(gameTime);
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
