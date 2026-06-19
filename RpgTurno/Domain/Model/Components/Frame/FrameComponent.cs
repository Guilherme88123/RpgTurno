using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.MenuComponents.Frame;

public class FrameComponent : BaseComponent
{
    private readonly List<BaseComponent> _children = new();

    public void AddChild(BaseComponent child)
    {
        _children.Add(child);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateChildren(gameTime);
    }

    private void UpdateChildren(GameTime gameTime)
    {
        foreach (var child in _children)
        {
            if (child is null)
                continue;

            child.Update(gameTime);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawChildren(spriteBatch);
    }

    private void DrawChildren(SpriteBatch spriteBatch)
    {
        foreach (var child in _children)
        {
            if (child is null)
                continue;

            child.Draw(spriteBatch);
        }
    }
}
