using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.Selection;

public class SelectionAreaComponent
{
    private const int _fixedSlice = 16;

    private readonly ResizableSpriteData _sprite;
    private Rectangle _destinationRectangle;

    public SelectionAreaComponent()
    {
        var selectionArea = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SelectionArea);
        _sprite = new ResizableSpriteData(selectionArea, ResizableSpriteType.Horizontal, _fixedSlice, _fixedSlice); //TODO: Usar sprite redimensionavel full

        _destinationRectangle = new(0, 0, _sprite.Width, _sprite.Height);
    }

    public void Draw()
    {
        _sprite.Draw(_destinationRectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchInterface);
    }

    public void SetDestinationRectangle(Rectangle destinationRectangle)
    {
        _destinationRectangle = FixRectangleWithFixedSlice(destinationRectangle);
    }

    private Rectangle FixRectangleWithFixedSlice(Rectangle destinationRectangle)
    {
        return new Rectangle(destinationRectangle.X - _fixedSlice, destinationRectangle.Y - _fixedSlice,
            destinationRectangle.Width + _fixedSlice * 2, destinationRectangle.Height + _fixedSlice * 2);
    }
}
