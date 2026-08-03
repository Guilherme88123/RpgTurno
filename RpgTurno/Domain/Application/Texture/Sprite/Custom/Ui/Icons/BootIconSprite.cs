using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Icons;

public class BootIconSprite : SpriteData
{
    public BootIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BootIcon))
    {
    }
}
