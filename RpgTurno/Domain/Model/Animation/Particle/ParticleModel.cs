using Domain.Dto.Global;
using Microsoft.Xna.Framework;

namespace Domain.Model.Animation.Particle;

public class ParticleModel
{
    public float PositionX { get; set; }
    public float PositionY { get; set; }

    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public float Scale { get; set; } = 1f;

    public float Lifetime { get; set; }      // tempo total
    public float LifetimeLeft { get; set; }  // tempo restante
    public bool IsAlive => LifetimeLeft > 0;

    public Color Color { get; set; } = Color.White;

    public void Update()
    {
        LifetimeLeft -= GlobalVariablesDto.DeltaTime;
        PositionX += VelocityX * GlobalVariablesDto.DeltaTime;
        PositionY += VelocityY * GlobalVariablesDto.DeltaTime;

        // fade out automático
        float progress = LifetimeLeft / Lifetime;
        Color = Color with { A = (byte)(255 * progress) };

        // encolhe com o tempo
        Scale = progress;
    }
}
