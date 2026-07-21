using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

public class SmallRedRoundButtonPressedSprite : SpriteData
{
    public SmallRedRoundButtonPressedSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallRedRoundButtonPressed))
    {
    }
}
