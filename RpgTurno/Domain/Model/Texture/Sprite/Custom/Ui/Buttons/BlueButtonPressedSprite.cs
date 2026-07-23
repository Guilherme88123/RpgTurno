using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

public class BlueButtonPressedSprite : ResizableSpriteData
{
    public BlueButtonPressedSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlueButtonPressed),
        ResizableSpriteType.Full,
        fixedHorizontal: 50,
        fixedVertical: 49,
        border: new BorderDefinition(15, 15, 14, 14),
        piecesGap: 64)
    {
    }
}
