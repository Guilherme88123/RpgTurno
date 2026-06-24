using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Domain.Dto.Global;

public static class GlobalVariablesDto
{
    public static Action<string>? ChangeScreen;
    public static Action<string>? PushScreen;
    public static Action? PopScreen;
    public static Action? Exit;

    public static GraphicsDeviceManager Graphics;
    public static Texture2D Pixel;

    public static GameTime GameTime;
    public static float DeltaTime;
    public static float AcumulatedDeltaTime;

    public static SpriteFont FontArial;
    public static SpriteFont FontThickPixels;
    public static SpriteFont FontLazyFox;
    public static SpriteFont FontStacked;
    public static SpriteFont GlobalFont => FontThickPixels;

    public static SpriteBatch SpriteBatchBackground;
    public static SpriteBatch SpriteBatchEntities;
    public static SpriteBatch SpriteBatchInterface;

    public static KeyboardState KeyboardState;
    public static MouseState MouseState;

    public static Dictionary<SpriteBatch, Vector2> SpriteBatchTransforms = new();

    public static ContentManager Content;
    public static IServiceProvider ServiceProvider;

    public static bool PreviousMouseDown;

    public static T GetService<T>() where T : notnull
        => ServiceProvider.GetRequiredService<T>();

    public static object GetService(Type type) => ServiceProvider.GetRequiredService(type);

    public static void Follow(SpriteBatch spriteBatch, Vector2 position)
    {
        SpriteBatchTransforms[spriteBatch] = position - new Vector2(GlobalOptionsDto.WidthSize / 2f, GlobalOptionsDto.HeightSize / 2f);
    }

    public static void ResetFollow(SpriteBatch spriteBatch)
    {
        SpriteBatchTransforms.Remove(spriteBatch);
    }

    public static Vector2? GetTransform(SpriteBatch spriteBatch)
    {
        if (SpriteBatchTransforms.TryGetValue(spriteBatch, out Vector2 position))
        {
            return position;
        }

        return null;
    }

    public static int GetBounceValue(float bounceAmplitude = 8f, float bounceSpeed = 6f)
    {
        var bounceFloat = bounceAmplitude * (Math.Cos(AcumulatedDeltaTime * bounceSpeed));
        return (int)bounceFloat;
    }
}
