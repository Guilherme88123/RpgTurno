using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Radio;

//TODO: Refatorar Radio quando tiver uma oportunidade de testar
public class RadioComponent : BaseComponent
{
    public string Text { get; set; }

    public int Max { get; set; } = 100;
    public int Min { get; set; } = 0;

    public int Value { get; set; } = 50;
    public Action<int> ValueUpdate { get; set; }

    public AnimationClip DotAnimation { get; set; }
    public Rectangle DotRectangle { get; set; }
    public bool IsDotPressed { get; set; }

    public AnimationClip LineAnimation { get; set; }
    public Rectangle LineRectangle { get; set; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateLineRectangle();
        UpdateDotRectangle();

        var mouse = Mouse.GetState();
        var mousePos = new Point(mouse.X, mouse.Y);

        var isDotHover = DotRectangle.Contains(mousePos);
        var isLineHover = LineRectangle.Contains(mousePos);

        var oldValue = Value;

        if (IsDotPressed)
        {
            int dotSize = Bounds.Height / 4;
            int left = LineRectangle.X;
            int right = LineRectangle.Right - dotSize;

            int clampedX = Math.Clamp(mouse.X, left, right);

            float percent = (clampedX - left) / (float)(right - left);
            Value = (int)(Min + percent * (Max - Min));
        }

        if (Value != oldValue)
        {
            ValueUpdate?.Invoke(Value);
        }

        if (mouse.LeftButton == ButtonState.Pressed && (isDotHover || isLineHover))
        {
            IsDotPressed = true;
        }
        else if (mouse.LeftButton == ButtonState.Released)
        {
            IsDotPressed = false;
        }
    }

    private void UpdateLineRectangle()
    {
        var border = Bounds.Width / 8;
        var lineHeight = Bounds.Height / 8;
        var lineY = Bounds.Y + Bounds.Height - lineHeight * 3;
        LineRectangle = new Rectangle(Bounds.X + border, lineY, Bounds.Width - border * 2, lineHeight);
    }

    private void UpdateDotRectangle()
    {
        int dotSize = Bounds.Height / 4;
        float percent = (float)(Value - Min) / (Max - Min);

        int left = LineRectangle.X;
        int right = LineRectangle.Right - dotSize;

        int dotX = (int)MathHelper.Lerp(left, right, percent);
        int dotY = LineRectangle.Y + LineRectangle.Height / 2 - dotSize / 2;

        DotRectangle = new Rectangle(dotX, dotY, dotSize, dotSize);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawLine(spriteBatch);

        if (DotAnimation is not null)
        {
            DrawDot(spriteBatch);
        }
    }

    protected void DrawDot(SpriteBatch spriteBatch)
    {
        if (DotAnimation is not null)
        {
            DrawDotAnimation(spriteBatch);
        }
        else
        {
            DrawDotRectangle(spriteBatch);
        }
    }

    protected void DrawDotAnimation(SpriteBatch spriteBatch)
    {
        DotAnimation.Draw(DotRectangle, Color, Rotation, SpriteEffects, spriteBatch);
    }

    protected void DrawDotRectangle(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GlobalVariablesDto.Pixel, DotRectangle, Color);
    }

    protected void DrawLine(SpriteBatch spriteBatch)
    {
        if (LineAnimation is not null)
        {
            DrawLineAnimation(spriteBatch);
        }
        else
        {
            DrawLineRectangle(spriteBatch);
        }
    }

    protected void DrawLineAnimation(SpriteBatch spriteBatch)
    {
        LineAnimation.Draw(LineRectangle, Color, Rotation, SpriteEffects, spriteBatch);
    }

    protected void DrawLineRectangle(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GlobalVariablesDto.Pixel, LineRectangle, Color.DarkGray);
    }

    protected string GetText() => $"{Text}: {Value}";
}
