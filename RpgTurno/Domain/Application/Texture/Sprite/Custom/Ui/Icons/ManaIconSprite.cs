using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Icons;

public class ManaIconSprite : SpriteData
{
    public ManaIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ManaIcon))
    {
    }
}
