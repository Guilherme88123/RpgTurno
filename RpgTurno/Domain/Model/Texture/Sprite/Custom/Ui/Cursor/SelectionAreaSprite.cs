using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Cursor;

public class SelectionAreaSprite : ResizableSpriteData
{
    public SelectionAreaSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SelectionArea), 
        ResizableSpriteType.Full, 
        fixedHorizontal: 32, 
        fixedVertical: 32, 
        border: null, 
        piecesGap: 0)
    {
    }
}
