using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite;

public class AnimationClip
{
    private readonly List<SpriteData> _frames = new();
    private readonly float _frameTime;

    private int _currentFrameIndex;
    private float _currentFrameTimeLeft;
    private bool _isPlaying = true;

    public bool IsLoop { get; set; } = true;

    public bool IsFinished => !IsLoop && _currentFrameIndex == _frames.Count - 1;

    public AnimationClip(List<SpriteData> frames, float frameTime = 0f, bool isLoop = true)
    {
        _frames = frames;
        _frameTime = frameTime;
        _currentFrameTimeLeft = frameTime;
        IsLoop = isLoop;
    }

    public AnimationClip(Texture2D texture, int framesX = 1, int framesY = 1, float frameTime = 0f, int row = 1, BorderDefinition border = null, bool isLoop = true)
    {
        _frames = new List<SpriteData>();
        _frameTime = frameTime;
        _currentFrameTimeLeft = frameTime;
        IsLoop = isLoop;

        var frameWidth = texture.Width / framesX;
        var frameHeight = texture.Height / framesY;

        for (int i = 0; i < framesX; i++)
        {
            var sourceRectangle = new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight);
            var sprite = new SpriteData(texture, sourceRectangle, border);
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

        if (_currentFrameTimeLeft > 0)
            return;

        _currentFrameTimeLeft += _frameTime;

        if (_currentFrameIndex < _frames.Count - 1)
            _currentFrameIndex++;
        else if (IsLoop)
            _currentFrameIndex = 0;
        else
            _isPlaying = false;

    }

    public void Draw(Rectangle destinationRectangle, Color color, float rotation, SpriteEffects drawEffect, SpriteBatch spriteBatch, float scale = 1.0f)
    {
        _frames[_currentFrameIndex].Draw(destinationRectangle, color, rotation, drawEffect, spriteBatch, scale);
    }
}
