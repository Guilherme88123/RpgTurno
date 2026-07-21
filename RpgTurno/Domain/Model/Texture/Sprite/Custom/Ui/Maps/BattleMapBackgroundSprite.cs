using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Maps;

public class BattleMapBackgroundSprite : SpriteData
{
    public BattleMapBackgroundSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BattleMapBackground))
    {
    }
}
