using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Manager;

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

    public void Add(object key, SpriteData sprite)
    {
        _animations[key] = new AnimationClip([sprite]);
        _currentKey ??= key;
    }

    public float GetAnimationTime(object key)
    {
        if (_animations.TryGetValue(key, out AnimationClip value))
        {
            return value.AnimationTime;
        }

        return 0f;
    }

    public bool HasKey(object key)
    {
        return _animations.TryGetValue(key, out AnimationClip value);
    }

    public void Update(object key)
    {
        if (_animations.TryGetValue(key, out AnimationClip value)) 
        {
            if (!Equals(key, _currentKey))
                value.Reset();

            value.Start(); 
            value.Update();

            _currentKey = key; 
        }
    }

    public void Draw(Rectangle rect, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch, Vector2 scale, Vector2 offset)
    {
        if (_currentKey is null)
            return;

        _animations[_currentKey].Draw(rect, color, rotation, drawEffect, spriteBatch, scale, offset);
    }
}
