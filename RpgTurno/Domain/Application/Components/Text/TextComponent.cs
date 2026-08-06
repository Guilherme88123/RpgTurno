using Domain.Dto.Global;
using Domain.Application.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Globalization;
using System.Text;

namespace Domain.Application.Components.Text;

public class TextComponent : BaseComponent
{
    public string Text { get; private set; }

    public bool IsPositionXByCenter { get; private set; }
    public bool IsPositionYByCenter { get; private set; }
    public SpriteFont Font { get; private set; }

    public Rectangle Bounds => new(base.Bounds.X, base.Bounds.Y, (int)Font.MeasureString(RemoveAccents(Text)).X, (int)Font.MeasureString(RemoveAccents(Text)).Y);

    public Color Color { get; set; } = Color.Black;

    public TextComponent(bool positionXByCenter = false, bool positionYByCenter = false)
    {
        IsPositionXByCenter = positionXByCenter;
        IsPositionYByCenter = positionYByCenter;
        Font = GlobalVariablesDto.GlobalFont;
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

        var textSize = Font.MeasureString(RemoveAccents(Text));

        var positionX = IsPositionXByCenter ? rawPositionX - textSize.X / 2 : rawPositionX;
        var positionY = IsPositionYByCenter ? rawPositionY - textSize.Y / 2 : rawPositionY;

        return ((int)positionX, (int)positionY);
    }

    private static string RemoveAccents(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var normalized = text.Normalize(NormalizationForm.FormD);

        var builder = new StringBuilder();

        foreach (var c in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);

            if (category != UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
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

        var position = new Vector2(Bounds.X, Bounds.Y) + Offset;

        var textSize = Font.MeasureString(RemoveAccents(Text));

        Vector2 origin = new Vector2(
            textSize.X * (ScaleX - 1) / (ScaleX * 2),
            textSize.Y * (ScaleY - 1) / (ScaleY * 2));

        var text = CanDraw(Text) ? Text : RemoveAccents(Text);

        text = text.Replace(" ", "  ");

        spriteBatch.DrawString(Font, text, position, Color, Rotation, origin, Scale, SpriteEffects, 1f);    
    }

    private bool CanDraw(string text)
    {
        try
        {
            Font.MeasureString(Text);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
