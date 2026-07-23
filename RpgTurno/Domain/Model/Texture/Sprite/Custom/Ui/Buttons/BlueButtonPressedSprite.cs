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
        fixedHorizontal: 48, 
        fixedVertical: 48, 
        border: new BorderDefinition(16, 16, 16, 16), 
        piecesGap: 64)
    {
    }
}
