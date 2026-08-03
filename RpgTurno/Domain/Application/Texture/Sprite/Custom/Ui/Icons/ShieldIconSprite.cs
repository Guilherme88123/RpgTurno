using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Icons;

public class ShieldIconSprite : SpriteData
{
    public ShieldIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ShieldIcon))
    {
    }
}
