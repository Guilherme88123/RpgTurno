using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

public class BlueButtonRegularSprite : ResizableSpriteData
{
    public BlueButtonRegularSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlueButtonRegular),
        ResizableSpriteType.Full,
        fixedHorizontal: 48,
        fixedVertical: 48,
        border: new BorderDefinition(16, 16, 16, 16),
        piecesGap: 64)
    {
    }
}
