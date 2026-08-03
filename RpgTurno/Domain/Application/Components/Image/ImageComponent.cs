using Domain.Application.Components.Base;
using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework;

namespace Domain.Application.Components.Image;

public class ImageComponent : BaseComponent
{
    public ImageComponent()
    {
        
    }

    public ImageComponent(SpriteData sprite, int width, int height)
    {
        AnimationManager.Add(true, new AnimationClip([sprite]));

        Bounds = new Rectangle(0, 0, width, height);
    }

    public ImageComponent(AnimationClip sprite, int width, int height)
    {
        AnimationManager.Add(true, sprite);

        Bounds = new Rectangle(0, 0, width, height);
    }

    public void SetImage(SpriteData sprite)
    {
        AnimationManager.Add(true, new AnimationClip([sprite]));
    }

    public void SetImage(AnimationClip sprite)
    {
        AnimationManager.Add(true, sprite);
    }
}
