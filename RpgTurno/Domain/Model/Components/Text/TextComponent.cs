using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Text;

public class TextComponent : BaseComponent
{
    public string Text { get; private set; }

    public void SetText(string text)
    {
        Text = text;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        DrawText(spriteBatch);
    }

    private void DrawText(SpriteBatch spriteBatch)
    {
        if (string.IsNullOrEmpty(Text))
            return;

        spriteBatch.DrawString(GlobalVariablesDto.GlobalFont, Text, new Vector2(Bounds.X, Bounds.Y), Color.Black);    
    }
}
