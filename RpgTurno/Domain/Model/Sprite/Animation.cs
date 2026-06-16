using Domain.Dto.Global;
using Domain.Model.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Animation;

public class Animation
{
    private readonly List<SpriteData> _frames = new();
    private readonly float _frameTime;

    private int _currentFrameIndex;
    private float _currentFrameTimeLeft;
    private bool _isPlaying = true;

    public Animation(List<SpriteData> frames, float frameTime)
    {
        _frames = frames;
        _frameTime = frameTime;
        _currentFrameTimeLeft = frameTime;
    }

    public Animation(Texture2D texture, int framesX = 1, int framesY = 1, float frameTime = 0f, int row = 1)
    {
        _frames = new List<SpriteData>();
        _frameTime = frameTime;
        _currentFrameTimeLeft = frameTime;

        var frameWidth = texture.Width / framesX;
        var frameHeight = texture.Height / framesY;

        for (int i = 0; i < framesX; i++)
        {
            var sourceRectangle = new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight);
            var sprite = new SpriteData(texture, sourceRectangle);
            _frames.Add(sprite);
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
        _currentFrameIndex = 0;
        _currentFrameTimeLeft = _frameTime;
    }

    public void Update()
    {
        if (!_isPlaying || _frames.Count <= 1) return;

        _currentFrameTimeLeft -= GlobalVariablesDto.DeltaTime;

        if (_currentFrameTimeLeft <= 0)
        {
            _currentFrameTimeLeft += _frameTime;
            _currentFrameIndex = (_currentFrameIndex + 1) % _frames.Count;
        }
    }

    public void Draw(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch)
    {
        _frames[_currentFrameIndex].Draw(destinationRectangle, color, rotation, drawEffect, spriteBatch);
    }
}
