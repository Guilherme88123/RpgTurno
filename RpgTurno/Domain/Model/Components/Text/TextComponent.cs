using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Text;

public class TextComponent : BaseComponent
{
    public string Text { get; private set; }

    public bool IsPositionXByCenter { get; private set; }
    public bool IsPositionYByCenter { get; private set; }
    private SpriteFont _font;

    public Color Color { get; set; } = Color.Black;

    public TextComponent(bool positionXByCenter = false, bool positionYByCenter = false)
    {
        IsPositionXByCenter = positionXByCenter;
        IsPositionYByCenter = positionYByCenter;
        _font = GlobalVariablesDto.GlobalFont;
    }

    public void SetText(string text)
    {
        Text = text;
    }

    public override void SetPosition(int positionX, int positionY)
    {
        if (IsPositionXByCenter || IsPositionYByCenter)
            (positionX, positionY) = GetPositionByCenter(positionX, positionY);

        base.SetPosition(positionX, positionY);
    }

    private (int, int) GetPositionByCenter(int rawPositionX, int rawPositionY)
    {
        if (string.IsNullOrEmpty(Text))
            return (rawPositionX, rawPositionY);

        var textSize = _font.MeasureString(Text);

        var positionX = IsPositionXByCenter ? rawPositionX - textSize.X / 2 : rawPositionX;
        var positionY = IsPositionYByCenter ? rawPositionY - textSize.Y / 2 : rawPositionY;

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
