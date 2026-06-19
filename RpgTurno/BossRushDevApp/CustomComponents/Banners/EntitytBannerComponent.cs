using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.Banners;

public class EntitytBannerComponent : FrameComponent
{
    public EntitytBannerComponent(Rectangle destinationRectangle)
    {
        var paperBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.PaperBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(paperBannerSprite, ResizableSpriteType.Full, 112, 112, new BorderDefinition(16, 16, 16, 16), 80)]));

        Bounds = destinationRectangle;
    }
}
