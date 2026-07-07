using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Text;

public class TextComponent : BaseComponent
{
    public string Text { get; private set; }

    private bool _positionByCenter;
    private SpriteFont _font;

    public Color Color { get; set; } = Color.Black;

    public TextComponent(bool positionByCenter = false)
    {
        _positionByCenter = positionByCenter;
        _font = GlobalVariablesDto.GlobalFont;
    }

    public void SetText(string text)
    {
        Text = text;
    }

    public override void SetPosition(int positionX, int positionY)
    {
        if (_positionByCenter)
            (positionX, positionY) = GetPositionByCenter(positionX, positionY);

        base.SetPosition(positionX, positionY);
    }

    private (int, int) GetPositionByCenter(int rawPositionX, int rawPositionY)
    {
        if (string.IsNullOrEmpty(Text))
            return (rawPositionX, rawPositionY);

        var textSize = _font.MeasureString(Text);

        var positionX = rawPositionX - textSize.X / 2;
        var positionY = rawPositionY - textSize.Y / 2;

        return ((int)positionX, (int)positionY);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (!IsVisible)
            return;

        DrawText(spriteBatch);
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
        if (string.IsNullOrEmpty(Text))
            return;

        spriteBatch.DrawString(_font, Text, new Vector2(Bounds.X, Bounds.Y), Color);    
    }
}
