using Domain.Dto.Global;
using Domain.Enum.Component.Cursor;
using Domain.Application.Components.Base;
using Domain.Application.Texture.Manager;
using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Domain.Application.Components.Cursor;

public class CursorComponent : BaseComponent
{
    private const int _hotspotX = 25;
    private const int _hotspotY = 21;

    public CursorStateType State { get; private set; }

    public CursorComponent(SpriteData normalSPrite, SpriteData hoverSprite, SpriteData blockSprite)
    {
        State = CursorStateType.Normal;

        AnimationManager.Add(CursorStateType.Normal, normalSPrite);
        AnimationManager.Add(CursorStateType.Hover, hoverSprite);
        AnimationManager.Add(CursorStateType.Block, blockSprite);

        AnimationManager.Update(State);

        Bounds = new Rectangle(0, 0, normalSPrite.Width, normalSPrite.Height);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var mousePoint = GetMousePoint();
        SetPosition(mousePoint.X, mousePoint.Y);

        AnimationManager.Update(State);
    }

    public void SetCursorState(CursorStateType state)
    {
        State = state;
    }

    private Point GetMousePoint()
    {
        var rawMousePoint = GlobalVariablesDto.MouseState.Position;

        float scaleX = (float)GlobalVariablesDto.Graphics.GraphicsDevice.Viewport.Width / 1920f;
        float scaleY = (float)GlobalVariablesDto.Graphics.GraphicsDevice.Viewport.Height / 1080f;

        var point = new Point(
            (int)((rawMousePoint.X - _hotspotX) / scaleX),
            (int)((rawMousePoint.Y - _hotspotY) / scaleY));

        return point;
    }
}
