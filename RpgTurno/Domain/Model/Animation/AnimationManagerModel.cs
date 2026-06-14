using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Animation;

public class AnimationManagerModel
{
    private readonly Dictionary<object, AnimationModel> Animations = new();

    private object _lastKey;

    public void AddAnimation(object key, AnimationModel animation)
    {
        Animations[key] = animation;
        _lastKey ??= key;
    }

    public void Update(object key)
    {
        if (Animations.ContainsKey(key))
        {
            Animations[key].Start();
            Animations[key].Update();
            _lastKey = key;
        }
    }

    public void Draw(Rectangle rect, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        Animations[_lastKey].Draw(rect, color, rotation, drawEffect, spriteBatch);
    }

    public bool HasAnimations()
    {
        return Animations.Count > 0;
    }
}
