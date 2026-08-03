using Domain.Dto.Global;
using Domain.Dto.Language;
using Domain.Application.Components.Image;
using Domain.Application.Components.Text;
using Domain.Application.Skill.Base.Unit;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Ribbons.Sword;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.Custom.Component.Play.Skill;

public class UsedSkillIndicatorComponent : ImageComponent
{
    private readonly TextComponent _skillNameText = new(positionXByCenter: true, positionYByCenter: true);

    public UsedSkillIndicatorComponent() : base(new BlueSwordRibbonSprite(), 480, 112)
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
        _skillNameText.SetText(LanguageManager.Get(skillName));
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

        UpdateBounceEffect();
    }

    private void UpdateBounceEffect()
    {
        var bounce = GlobalVariablesDto.GetBounceValue(bounceAmplitude: 0.0011f, bounceSpeed: 4f);

        OffsetX += bounce * 100;
        ScaleY += bounce;

        _skillNameText.OffsetX = OffsetX;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _skillNameText.Draw(spriteBatch);
    }
}
