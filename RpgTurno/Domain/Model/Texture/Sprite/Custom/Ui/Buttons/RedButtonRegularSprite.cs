using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

public class RedButtonRegularSprite : ResizableSpriteData
{
    public RedButtonRegularSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.RedButtonRegular), 
        ResizableSpriteType.Full, fixedHorizontal: 64, fixedVertical: 64, border: null, piecesGap: 64)
    {
    }
}
