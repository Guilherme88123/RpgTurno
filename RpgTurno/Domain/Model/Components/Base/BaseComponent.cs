using Domain.Dto.Global;
using Domain.Interface.Cursor;
using Domain.Model.Components.Base.Hover;
using Domain.Model.Texture.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Base;

public class BaseComponent
{
    public Rectangle Bounds { get; protected set; }

    protected ICursorManager CursorManager => GlobalVariablesDto.GetService<ICursorManager>();

    public HoverState HoverState { get; } = new();
    public HoverAnimation HoverAnimation { get; } = new();

    public bool IsVisible { get; set; } = true;
    public bool IsEnable { get; set; } = true;

    protected AnimationManager AnimationManager { get; } = new();

    public Color Color { get; set; } = Color.White;
    public Color TextColor { get; set; } = Color.Black;
    public float Rotation { get; set; }
    public float ScaleX { get; set; } = 1f;
    public float ScaleY { get; set; } = 1f;
    public float OffsetX { get; set; }
    public float OffsetY { get; set; }

    public Vector2 Scale => new(ScaleX, ScaleY);
    public Vector2 Offset => new(OffsetX, OffsetY);

    public SpriteEffects SpriteEffects { get; set; }

    public BaseComponent()
    {
        Bounds = new(0, 0, 256, 128);
    }

    public virtual void Update(GameTime gameTime)
    {
        if (!IsEnable)
            return;

        HoverState.Update(Bounds);
        HoverAnimation.Update(this, gameTime);
    }

    public virtual void SetPosition(int positionX, int positionY)
    {
        var newBound = new Rectangle(positionX, positionY, Bounds.Width, Bounds.Height);
        Bounds = newBound;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (!IsVisible)
            return;

        AnimationManager.Draw(Bounds, Color, Rotation, SpriteEffects, spriteBatch, Scale, Offset);
    }
}
