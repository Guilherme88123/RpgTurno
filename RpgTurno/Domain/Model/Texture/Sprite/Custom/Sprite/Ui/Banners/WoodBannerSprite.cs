using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Banners;

public class WoodBannerSprite : ResizableSpriteData
{
    public WoodBannerSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WoodBanner), 
        ResizableSpriteType.Full, fixedHorizontal: 128, fixedVertical: 128, border: null, piecesGap: 64)
    {
    }
}
