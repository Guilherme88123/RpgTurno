using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;

namespace Domain.Model.Components.Image;

public class ImageComponent : BaseComponent
{
    public ImageComponent(SpriteData sprite, int width, int height)
    {
        AnimationManager.Add(true, new AnimationClip([sprite]));

        Bounds = new Rectangle(0, 0, width, height);
    }
}
