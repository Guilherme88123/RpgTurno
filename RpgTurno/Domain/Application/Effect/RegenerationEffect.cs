using Domain.Application.Effect.Base;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Application.Effect;

public class RegenerationEffect : BaseEffect
{
    public override string Name => "Regeneration";
    public override string Description => "This unit is \nslowly recovering \nfrom its wounds";
    public override SpriteData Icon => new HeartIconSprite();

    public RegenerationEffect() : base(duration: 2)
    {
    }

    public override void OnTurnStart(BaseUnitEntity unit)
    {
        unit.RecieveHeal(5);
    }
}
