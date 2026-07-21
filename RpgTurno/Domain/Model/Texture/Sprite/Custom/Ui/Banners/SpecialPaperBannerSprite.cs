using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Banners;

public class SpecialPaperBannerSprite : ResizableSpriteData
{
    public SpecialPaperBannerSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SpecialPaperBanner), 
        ResizableSpriteType.Full, fixedHorizontal: 64, fixedVertical: 64, border: null, piecesGap: 64)
    {
    }
}
