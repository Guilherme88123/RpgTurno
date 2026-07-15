using Domain.Dto.Global;
using Domain.Interface.Cursor;
using Domain.Model.Texture.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Base;

public class BaseComponent
{
    public Rectangle Bounds { get; protected set; }

    protected ICursorManager CursorManager => GlobalVariablesDto.GetService<ICursorManager>();

    protected HoverState HoverState { get; } = new();

    public bool IsVisible { get; set; } = true;
    public bool IsEnable { get; set; } = true;

    protected AnimationManager AnimationManager { get; } = new();

    protected Color Color { get; set; } = Color.White;
    protected float Rotation { get; set; }
    protected SpriteEffects SpriteEffects { get; set; }

    public virtual void Update(GameTime gameTime)
    {
        if (!IsEnable)
            return;

        HoverState.Update(Bounds);
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

        AnimationManager.Draw(Bounds, Color, Rotation, SpriteEffects, spriteBatch);
    }
}
