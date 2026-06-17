using Domain.Dto.Global;
using Domain.Model.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Base;

public class BaseComponent
{
    public Rectangle Rectangle { get; set; } = new(0, 0, 100, 50);
    public Color Color { get; set; } = Color.White;
    public Color TextColor { get; set; } = Color.Black;
    public string Text { get; set; }

    public bool CanHover { get; set; } = true;
    public bool IsHover { get; set; }

    public const float Delay = 0.1f;
    public float _currentDelay { get; set; }
    protected float _currentDelayClickAnimation { get; set; }
    protected bool HasClicked => _currentDelayClickAnimation > 0;

    public AnimationClip NormalAnimation { get; set; }
    public AnimationClip HoverAnimation { get; set; }
    public AnimationClip ClickAnimation { get; set; }

    protected SoundEffect? ClickSound { get; set; }

    public SpriteBatch SpriteBatch { get; set; }
    public SpriteFont SpriteFont { get; set; }

    public float ActualAngle { get; set; }
    public SpriteEffects DrawEffect { get; set; } = SpriteEffects.None;

    public bool IsVisible { get; set; } = true;

    public virtual void Update()
    {
        if (!IsVisible) return;

        _currentDelay -= GlobalVariablesDto.DeltaTime;
        _currentDelayClickAnimation -= GlobalVariablesDto.DeltaTime;

        if (CanHover)
        {
            IsHover = IsHovering();
        }
    }

    public virtual void Draw()
    {
        if (!IsVisible) return;

        if (NormalAnimation is not null)
        {
            DrawAnimation();
        }
        else
        {
            DrawRectangle();
        }

        var text = GetText();

        if (!string.IsNullOrEmpty(text))
        {
            DrawText(text);
        }
    }

    protected virtual bool IsHovering()
    {
        var mouse = Mouse.GetState();
        Vector2 mouseScreen = new(mouse.X, mouse.Y);

        return Rectangle.Contains(mouseScreen);
    }

    protected virtual void DrawText(string text)
    {
        SpriteBatch = SpriteBatch is null ? GlobalVariablesDto.SpriteBatchInterface : SpriteBatch;
        SpriteFont = SpriteFont is null ? GlobalVariablesDto.FontArial : SpriteFont;

        var textSize = SpriteFont.MeasureString(text);

        int clickGap = HasClicked ? 10 : 0;

        var textPosition = new Vector2(
            Rectangle.X + (Rectangle.Width - textSize.X) / 2,
            Rectangle.Y + Rectangle.Height / 2 - (int)(textSize.Y / 1.5) + clickGap);
        SpriteBatch.DrawString(SpriteFont, text, textPosition, TextColor);
    }

    protected void DrawRectangle()
    {
        SpriteBatch = SpriteBatch is null ? GlobalVariablesDto.SpriteBatchInterface : SpriteBatch;

        SpriteBatch.Draw(GlobalVariablesDto.Pixel, Rectangle, IsHover ? Color * 0.7f : Color);
    }

    protected void DrawAnimation()
    {
        SpriteBatch = SpriteBatch is null ? GlobalVariablesDto.SpriteBatchInterface : SpriteBatch;

        if (HasClicked && ClickAnimation is not null)
        {
            ClickAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, SpriteBatch);
            return;
        }

        if (IsHover && HoverAnimation is not null)
        {
            HoverAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, SpriteBatch);
            return;
        }

        NormalAnimation.Draw(Rectangle, Color, ActualAngle, DrawEffect, SpriteBatch);
    }

    protected virtual string GetText() => Text;
}
