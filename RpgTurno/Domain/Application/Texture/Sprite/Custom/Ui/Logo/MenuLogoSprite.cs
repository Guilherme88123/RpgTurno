using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Logo;

public class MenuLogoSprite : SpriteData
{
    public MenuLogoSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.MenuLogo))
    {
    }
}
