using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Buttons;

public class BlueButtonRegularSprite : ResizableSpriteData
{
    public BlueButtonRegularSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlueButtonRegular), 
        ResizableSpriteType.Full, fixedHorizontal: 64, fixedVertical: 64, border: null, piecesGap: 64)
    {
    }
}
