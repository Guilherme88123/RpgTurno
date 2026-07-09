using Domain.Dto.Global;
using Domain.Enum.Component.Cursor;
using Domain.Model.Components.Base;
using Domain.Model.Texture.Manager;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Cursor;

public class CursorComponent : BaseComponent
{
    private const int _hotspotX = 25;
    private const int _hotspotY = 21;

    private CursorStateType _state;

    public CursorComponent(Texture2D normalSPrite, Texture2D hoverSprite, Texture2D blockSprite)
    {
        _state = CursorStateType.Normal;

        AnimationManager.Add(CursorStateType.Normal, new AnimationClip(normalSPrite));
        AnimationManager.Add(CursorStateType.Hover, new AnimationClip(hoverSprite));
        AnimationManager.Add(CursorStateType.Block, new AnimationClip(blockSprite));

        AnimationManager.Update(_state);

        Bounds = new Rectangle(0, 0, normalSPrite.Width, normalSPrite.Height);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var mouse = GlobalVariablesDto.MouseState;
        var cursorRectangle = new Rectangle(mouse.X - _hotspotX, mouse.Y - _hotspotY, Bounds.Width, Bounds.Height);
        Bounds = cursorRectangle;

        AnimationManager.Update(_state);
    }

    public void SetCursorState(CursorStateType state)
    {
        _state = state;
    }
}
