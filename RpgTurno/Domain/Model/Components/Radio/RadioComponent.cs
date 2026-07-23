using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Model.Components.Base;
using Domain.Model.Components.Text;
using Domain.Model.Sound.Base;
using Domain.Model.Sound.Ui;
using Domain.Model.Texture.Sprite;
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

    public SpriteData DotSprite { get; set; }
    public Rectangle DotRectangle { get; set; }
    public bool IsDotPressed { get; set; }

    public SpriteData LineAnimation { get; set; }
    public Rectangle LineRectangle { get; set; }

    public readonly TextComponent Text = new(positionXByCenter: true, positionYByCenter: true);
    private string _baseText;

    private bool _wasClick;

    private readonly SoundEffectData ClickSoundEffect = new ButtonClickSoundEffect();
    private readonly SoundEffectData HoverSoundEffect = new ButtonHoverSoundEffect();

    public RadioComponent()
    {
        HoverState.OnHoverIn += OnHoverIn;

        HoverAnimation.AffectScaleX = true;
        HoverAnimation.AffectScaleY = true;
        HoverAnimation.AffectOffsetY = true;
        HoverAnimation.AffectTextColor = true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        Text.Update(gameTime);
        Text.Color = TextColor;
        Text.OffsetY = OffsetY;

        UpdateLineRectangle();
        UpdateDotRectangle();

        var mouse = Mouse.GetState();
        var mousePos = new Point(mouse.X, mouse.Y);

        var isDotHover = DotRectangle.Contains(mousePos);
        var isLineHover = LineRectangle.Contains(mousePos);

        if (isDotHover || isLineHover)
            CursorManager.RequestHover();

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
            if (!_wasClick)
                ClickSoundEffect?.Play();

            IsDotPressed = true;

            _wasClick = true;
        }
        else if (mouse.LeftButton == ButtonState.Released)
        {
            IsDotPressed = false;
            _wasClick = false;
        }

        Text.SetText(GetText());
        ReloadPositionText();
    }

    #region Hover

    private void OnHoverIn()
    {
        HoverSoundEffect.Play();
    }

    #endregion

    private void UpdateLineRectangle()
    {
        var border = Bounds.Width / 8;
        var lineHeight = Bounds.Height / 6;
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
        DrawDot(spriteBatch);
        Text.Draw(spriteBatch);
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
        LineAnimation.Draw(LineRectangle, Color, Rotation, SpriteEffects, spriteBatch, Scale, Offset);
    }

    protected void DrawLineRectangle(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GlobalVariablesDto.Pixel, LineRectangle, Color.DarkGray);
    }

    protected void DrawDot(SpriteBatch spriteBatch)
    {
        if (DotSprite is not null)
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
        DotSprite.Draw(DotRectangle, Color, Rotation, SpriteEffects, spriteBatch, Scale, Offset);
    }

    protected void DrawDotRectangle(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GlobalVariablesDto.Pixel, DotRectangle, Color);
    }

    protected string GetText() => $"{_baseText}: {Value}";

    public void SetText(string text)
    {
        _baseText = text;
    }

    #region Position

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        SetPositionText(positionX, positionY);
    }

    private void ReloadPositionText()
    {
        SetPositionText(Bounds.X, Bounds.Y);
    }

    private void SetPositionText(int positionX, int positionY)
    {
        Text.SetPosition(positionX + Bounds.Width / 2, positionY + Bounds.Height / 3);
    }

    #endregion
}
