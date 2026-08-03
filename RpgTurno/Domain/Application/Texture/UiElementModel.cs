using Domain.Model.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Animation;

public class UiElementModel : BaseModel
{
    public AnimationModel Animation { get; set; }

    public Rectangle Rectangle { get; set; }
    public Color Color { get; set; } = Color.White;
    public float ActualAngle { get; set; }
    public SpriteEffects SpriteEffects { get; set; }
    public SpriteBatch SpriteBatch { get; set; }
    public bool IsVisible { get; set; } = true;

    public UiElementModel(AnimationModel animation, Rectangle rectangle, SpriteBatch spriteBatch)
    {
        Animation = animation;
        Rectangle = rectangle;
        SpriteBatch = spriteBatch;
    }

    public UiElementModel(AnimationModel animation, Rectangle rectangle, SpriteBatch spriteBatch, bool isVisible)
    {
        Animation = animation;
        Rectangle = rectangle;
        SpriteBatch = spriteBatch;
        IsVisible = isVisible;
    }

    public UiElementModel(AnimationModel animation, Rectangle rectangle, SpriteBatch spriteBatch, Color color)
    {
        Animation = animation;
        Rectangle = rectangle;
        SpriteBatch = spriteBatch;
        Color = color;
    }

    public void Draw()
    {
        if (!IsVisible) return;

        Animation.Draw(Rectangle, Color, ActualAngle, SpriteEffects, SpriteBatch);
    }
}
