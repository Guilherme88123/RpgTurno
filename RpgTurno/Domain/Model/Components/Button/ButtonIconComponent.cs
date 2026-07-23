using Application.Model.MenuElements.Button;
using Domain.Enum.Component.Button;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Button;

public class ButtonIconComponent : ButtonComponent
{
    public SpriteData Icon { get; private set; }

    public ButtonIconComponent(SpriteData icon)
    {
        Icon = icon;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawIcon(spriteBatch);
    }

    private void DrawIcon(SpriteBatch spriteBatch)
    {
        if (Icon is null)
            return;

        var pressedOffset = State == ButtonInteractionState.Pressed ? 10 : 0;

        var iconWidth = Bounds.Width / 3;
        var iconHeight = Bounds.Height / 3;

        var iconRectangle = new Rectangle(
            Bounds.Center.X - iconWidth / 2,
            Bounds.Center.Y - iconHeight / 2 + pressedOffset - 8,
            iconWidth,
            iconHeight);

        Icon.Draw(iconRectangle, Color, Rotation, SpriteEffects, spriteBatch, Scale, Offset);
    }
}
