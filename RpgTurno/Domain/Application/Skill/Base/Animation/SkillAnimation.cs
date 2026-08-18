using Domain.Application.Sound.Base;
using Domain.Application.Texture.Sprite;

namespace Domain.Application.Skill.Base.Animation;

public record SkillAnimation
{
    public AnimationClip TargetAnimation { get; set; }
    public AnimationClip SenderAnimation { get; set; }
    public SoundEffectData SoundEffect { get; set; }
    public bool IsRanged { get; set; }

    public SkillAnimation(AnimationClip targetAnimation, AnimationClip senderAnimation, SoundEffectData soundEffect, bool isRanged)
    {
        TargetAnimation = targetAnimation;
        SenderAnimation = senderAnimation;
        SoundEffect = soundEffect;
        IsRanged = isRanged;
    }
}
