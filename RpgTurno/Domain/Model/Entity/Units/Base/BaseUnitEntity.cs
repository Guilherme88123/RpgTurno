using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Components.Custom.HealthBar;
using Domain.Model.Entity.Base;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Base;

public class BaseUnitEntity : BaseEntity
{
    public int MaxHealth { get; set; } = 10;
    public int Health { get; set; }

    public int Damage { get; set; } = 4;

    private HealthBarComponent _healthBar;

    public BaseUnitEntity()
    {
        Health = MaxHealth; 
        
        var baseTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarBase);
        var fillTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarFill);
        _healthBar = new HealthBarComponent(baseTexture, fillTexture, width: 200, height: 64, offsetY: -20);

    }

    public override void Update()
    {
        base.Update();

        CreatureState = CreatureStateType.Idle;
    }

    public override void Draw()
    {
        base.Draw(); 
        _healthBar.Draw(Center, Health, MaxHealth, GlobalVariablesDto.SpriteBatchInterface);
    }
}
