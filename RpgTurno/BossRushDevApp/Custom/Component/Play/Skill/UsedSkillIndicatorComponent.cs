using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.Skill.Base.Unit;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Ribbons.Sword;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.Custom.Component.Play.Skill;

public class UsedSkillIndicatorComponent : ImageComponent
{
    private readonly TextComponent _skillNameText = new(positionXByCenter: true, positionYByCenter: true);

    public UsedSkillIndicatorComponent() : base(new BlueSwordRibbonSprite(), 512, 128)
    {
    }

    public void SetUsedSkill(UnitSkill unitSkill, bool isEnemy)
    {
        SetSpriteByUnitAllignment(isEnemy);
        SetSkillName(unitSkill.Definition.Name);
    }

    private void SetSpriteByUnitAllignment(bool isEnemy)
    {
        SpriteData sprite = isEnemy ? new RedSwordRibbonSprite() : new BlueSwordRibbonSprite();
        SetImage(sprite);
    }

    private void SetSkillName(string skillName)
    {
        _skillNameText.SetText(skillName);
        _skillNameText.SetPosition(Bounds.Center.X, Bounds.Center.Y);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        _skillNameText.SetPosition(Bounds.Center.X, Bounds.Center.Y);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _skillNameText.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _skillNameText.Draw(spriteBatch);
    }
}
