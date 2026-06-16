using Application.Model.MenuElements.Base;
using Domain.Dto.Global;
using Domain.Model.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Radio;

public class RadioComponent : BaseComponent
{
    public int Max { get; set; } = 100;
    public int Min { get; set; } = 0;

    public int Value { get; set; } = 50;
    public Action<int> ValueUpdate { get; set; }

    public Animation DotAnimation { get; set; }
    public Rectangle DotRectangle { get; set; }
    public bool IsDotPressed { get; set; }

    public Animation LineAnimation { get; set; }
    public Rectangle LineRectangle { get; set; }

    public override void Update()
    {
        base.Update();

        UpdateLineRectangle();
        UpdateDotRectangle();

        var mouse = Mouse.GetState();
        var mousePos = new Point(mouse.X, mouse.Y);

        var isDotHover = DotRectangle.Contains(mousePos);
        var isLineHover = LineRectangle.Contains(mousePos);

        var oldValue = Value;

        if (IsDotPressed)
        {
            int dotSize = Rectangle.Height / 4;
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
            if (!IsDotPressed)
            {
                ClickSound?.Play(GlobalOptionsDto.SfxVolumeFloat, 0f, 0f);
            }
            IsDotPressed = true;
        }
        else if (mouse.LeftButton == ButtonState.Released)
        {
            IsDotPressed = false;
        }
    }

    private void UpdateLineRectangle()
    {
        var border = Rectangle.Width / 8;
        var lineHeight = Rectangle.Height / 8;
        var lineY = Rectangle.Y + Rectangle.Height - lineHeight * 3;
        LineRectangle = new Rectangle(Rectangle.X + border, lineY, Rectangle.Width - border * 2, lineHeight);
    }

    private void UpdateDotRectangle()
    {
        int dotSize = Rectangle.Height / 4;
        float percent = (float)(Value - Min) / (Max - Min);

        int left = LineRectangle.X;
        int right = LineRectangle.Right - dotSize;

        int dotX = (int)MathHelper.Lerp(left, right, percent);
        int dotY = LineRectangle.Y + LineRectangle.Height / 2 - dotSize / 2;

        DotRectangle = new Rectangle(dotX, dotY, dotSize, dotSize);
    }

    public override void Draw()
    {
        base.Draw();

        DrawLine();

        if (DotAnimation is not null)
        {
            DrawDot();
        }
    }

    protected void DrawDot()
    {
        if (DotAnimation is not null)
        {
            DrawDotAnimation();
        }
        else
        {
            DrawDotRectangle();
        }
    }

    protected void DrawDotAnimation()
    {
        DotAnimation.Draw(DotRectangle, Color, ActualAngle, DrawEffect, SpriteBatch);
    }

    protected void DrawDotRectangle()
    {
        SpriteBatch.Draw(GlobalVariablesDto.Pixel, DotRectangle, Color);
    }

    protected void DrawLine()
    {
        if (LineAnimation is not null)
        {
            DrawLineAnimation();
        }
        else
        {
            DrawLineRectangle();
        }
    }

    protected void DrawLineAnimation()
    {
        LineAnimation.Draw(LineRectangle, Color, ActualAngle, DrawEffect, SpriteBatch);
    }

    protected void DrawLineRectangle()
    {
        SpriteBatch.Draw(GlobalVariablesDto.Pixel, LineRectangle, Color.DarkGray);
    }

    protected override string GetText() => $"{Text}: {Value}";
}
