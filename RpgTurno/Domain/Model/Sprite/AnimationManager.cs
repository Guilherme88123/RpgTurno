using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Animation;

public class AnimationManager
{
    private readonly Dictionary<object, Animation> Animations = new();

    private object _lastKey;

    public void AddAnimation(object key, Animation animation)
    {
        Animations[key] = animation;
        _lastKey ??= key;
    }

    public void Update(object key)
    {
        if (Animations.TryGetValue(key, out Animation? value))
        {
            value.Start();
            value.Update();
            _lastKey = key;
        }
    }

    public void Draw(Rectangle rect, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        Animations[_lastKey].Draw(rect, color, rotation, drawEffect, spriteBatch);
    }

    public bool HasAnimations()
    {
        return Animations.Any();
    }
}
