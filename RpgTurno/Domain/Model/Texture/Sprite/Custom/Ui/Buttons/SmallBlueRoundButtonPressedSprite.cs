using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

public class SmallBlueRoundButtonPressedSprite : SpriteData
{
    public SmallBlueRoundButtonPressedSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBlueRoundButtonPressed))
    {
    }
}
