using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.CustomComponents.Banners;

public class AttackSelectBannerComponent : FrameComponent
{
    private const int _fixedSlice = 112;

    public AttackSelectBannerComponent()
    {
        var woodBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WoodBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(woodBannerSprite, ResizableSpriteType.Full, _fixedSlice, _fixedSlice, new BorderDefinition(16, 16, 16, 16), 80)]));

        Bounds = new Rectangle(0, 0, 550, 400);
    }
}
