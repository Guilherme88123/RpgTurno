using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;

public class CriticalIconSprite : SpriteData
{
    public CriticalIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.CriticalIcon))
    {
    }
}
