using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Icons;

public class ConfirmIconSprite : SpriteData
{
    public ConfirmIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ConfirmIcon))
    {
    }
}
