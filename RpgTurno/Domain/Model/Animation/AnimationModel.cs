using Domain.Dto.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Animation;

public class AnimationModel
{
    private readonly Texture2D Texture;
    private readonly List<Rectangle> Frames = new();
    private readonly int QuantityFrames;
    private readonly float FrameTime;

    private int _currentFrame;
    private float _currentFrameTimeLeft;
    private bool _isPlaying = true;

    public AnimationModel(Texture2D texture, int framesX = 1, int framesY = 1, float frameTime = 0f, int row = 1)
    {
        Texture = texture;
        QuantityFrames = framesX;
        FrameTime = frameTime;
        _currentFrameTimeLeft = FrameTime;

        var frameWidth = Texture.Width / framesX;
        var frameHeight = Texture.Height / framesY;

        for (int i = 0; i < QuantityFrames; i++)
        {
            Frames.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
        }
    }

    public void Start()
    {
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
    }

    public void Reset()
    {
        _currentFrame = 0;
        _currentFrameTimeLeft = FrameTime;
    }

    public void Update()
    {
        bool hasOneFrame = QuantityFrames <= 1;

        if (!_isPlaying || hasOneFrame) return;

        _currentFrameTimeLeft -= GlobalVariablesDto.DeltaTime;

        if (_currentFrameTimeLeft <= 0)
        {
            _currentFrameTimeLeft += FrameTime;
            _currentFrame = (_currentFrame + 1) % QuantityFrames;
        }
    }

    public void Draw(Rectangle rect, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        var source = Frames[_currentFrame];

        var scaleX = (float)rect.Width / source.Width;
        var scaleY = (float)rect.Height / source.Height;

        Vector2? cameraOffset = GlobalVariablesDto.GetTransform(spriteBatch);

        Vector2 position = new Vector2(rect.X - rect.Width, rect.Y - rect.Height);

        spriteBatch.Draw(
            Texture,
            position - (cameraOffset ?? Vector2.Zero),
            source,
            color,
            rotation,
            Vector2.Zero,
            new Vector2(scaleX, scaleY),
            drawEffect,
            0f);
    }
}
