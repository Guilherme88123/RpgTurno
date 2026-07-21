using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Logo;

public class MenuLogoSprite : SpriteData
{
    public MenuLogoSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.MenuLogo))
    {
    }
}
