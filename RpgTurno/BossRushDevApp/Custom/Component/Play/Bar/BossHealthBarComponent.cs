using Domain.Dto.Language;
using Domain.Model.Components.ProgressBar;
using Domain.Model.Components.Text;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite.Custom.Ui.Bars;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.Custom.Component.Play.Bar;

public class BossHealthBarComponent : ProgressBarComponent
{
    private readonly TextComponent _title = new(positionXByCenter: true, positionYByCenter: true);

    public BossHealthBarComponent() : base(new BigBarFillSprite(), 0, 0, 32)
    {
        AnimationManager.Add(true, new BigBarBaseSprite());

        Bounds = new Rectangle(0, 0, 480, 96);
    }

    public void SetBossUnit(BaseUnitEntity boss)
    {
        SetValues(boss.Stats.MaxHealth, boss.Stats.CurrentHealth);
        _title.SetText(LanguageManager.Get(boss.Name));

        SetPosition(Bounds.X, Bounds.Y);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _title.Update(gameTime);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        _title.SetPosition(Bounds.Center.X, Bounds.Center.Y);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _title.Draw(spriteBatch);
    }
}
