using Domain.Model.Effect.Base;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;

namespace Domain.Model.Effect;

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
