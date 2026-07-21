using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Buttons;

public class SmallRedRoundButtonRegularSprite : SpriteData
{
    public SmallRedRoundButtonRegularSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallRedRoundButtonRegular))
    {
    }
}
