using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Manager;

public class AnimationManager
{
    public bool IsEmpty => _animations.Count == 0;

    private readonly Dictionary<object, AnimationClip> _animations = new();
    private object _currentKey = null;

    public void Add(object key, AnimationClip animation)
    {
        _animations[key] = animation;
        _currentKey ??= key;
    }

    public void Update(object key)
    {
        if (_animations.TryGetValue(key, out AnimationClip value)) 
        { 
            value.Start(); 
            value.Update();
            _currentKey = key; 
        }
    }

    public void Draw(Rectangle rect, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        _animations[_currentKey].Draw(rect, color, rotation, drawEffect, spriteBatch);
    }
}
