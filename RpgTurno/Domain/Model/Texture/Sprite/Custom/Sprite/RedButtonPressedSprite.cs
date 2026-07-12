using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite;

public class RedButtonPressedSprite : ResizableSpriteData
{
    public RedButtonPressedSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.RedButtonPressed), 
        ResizableSpriteType.Full, fixedHorizontal: 64, fixedVertical: 64, border: null, piecesGap: 64)
    {
    }
}
