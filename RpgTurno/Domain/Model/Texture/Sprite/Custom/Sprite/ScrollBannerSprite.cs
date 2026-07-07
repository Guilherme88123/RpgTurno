using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite;

public class ScrollBannerSprite : ResizableSpriteData
{
    public ScrollBannerSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ScrollBanner), 
        ResizableSpriteType.Full, fixedHorizontal: 112, fixedVertical: 112, border: new BorderDefinition(16, 16, 16, 16), piecesGap: 80)
    {
    }
}
