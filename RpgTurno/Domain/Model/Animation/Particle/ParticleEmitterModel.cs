using Domain.Dto.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Animation.Particle;

public class ParticleEmitterModel
{
    private readonly List<ParticleModel> _particles = new();
    private readonly Texture2D _texture;
    private readonly Random _random = new();

    public ParticleEmitterModel(Texture2D texture)
    {
        _texture = texture;
    }

    public void Emit(Vector2 position, Color color, int quantity = 10)
    {
        for (int i = 0; i < quantity; i++)
        {
            float angle = (float)(_random.NextDouble() * Math.PI * 2);
            float speed = 50f + (float)_random.NextDouble() * 150f;

            _particles.Add(new ParticleModel
            {
                PositionX = position.X,
                PositionY = position.Y,
                VelocityX = (float)Math.Cos(angle) * speed,
                VelocityY = (float)Math.Sin(angle) * speed,
                Lifetime = 0.5f + (float)_random.NextDouble() * 0.5f,
                LifetimeLeft = 0.5f + (float)_random.NextDouble() * 0.5f,
                Scale = 0.5f + (float)_random.NextDouble() * 0.5f,
                Color = color,
            });
        }
    }

    public void Update()
    {
        _particles.ForEach(p => p.Update());
        _particles.RemoveAll(p => !p.IsAlive);
    }

    public void Draw()
    {
        Vector2? cameraOffset = GlobalVariablesDto.GetTransform(GlobalVariablesDto.SpriteBatchEntities);

        foreach (var p in _particles)
        {
            GlobalVariablesDto.SpriteBatchInterface.Draw(
                _texture,
                new Vector2(p.PositionX, p.PositionY) - (cameraOffset ?? Vector2.Zero),
                null,
                p.Color,
                0f,
                new Vector2(0.5f, 0.5f),
                p.Scale,
                SpriteEffects.None,
                0f);
        }
    }
}
