using Domain.Const.Screen;
using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RpgTurno.Screen.Map.World.Stage;

namespace RpgTurno.Screen.Map.World.Player;

public class MapPlayerData
{
    public StageMapNode CurrentStage { get; set; }
    public Vector2 Position { get; set; }

    private AnimationClip _animation;
    private int _size = 128;

    public MapPlayerData()
    {
        _animation = new AnimationClip([new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorAvatar))]);
    }

    public void SetCurrentStage(StageMapNode currentStage)
    {
        CurrentStage = currentStage;
        Position = currentStage.Position;
    }

    public void Update()
    {
        _animation.Update();
        VerifyEnteringStage();
    }

    public void VerifyEnteringStage()
    {
        var teclado = Keyboard.GetState();

        if (teclado.IsKeyDown(Keys.Enter))
            GlobalVariablesDto.PushScreen(ScreenConst.PlayScreen);
    }

    public void Draw()
    {
        var destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, _size, _size);
        _animation.Draw(destinationRectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchEntities);
    }
}
