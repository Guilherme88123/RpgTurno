using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;

public class FortressIconSprite : SpriteData
{
    public FortressIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.FortressIcon))
    {
    }
}
