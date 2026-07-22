using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Audio;

namespace Domain.Model.Skill.Base.Animation;

public record SkillAnimation
{
    public AnimationClip TargetAnimation { get; set; }
    public AnimationClip SenderAnimation { get; set; }
    public SoundEffect SoundEffect { get; set; }
    public bool IsRanged { get; set; }
    public float ExecutionTime { get; set; }

    public SkillAnimation(AnimationClip targetAnimation, AnimationClip senderAnimation, SoundEffect soundEffect, bool isRanged, float executionTime)
    {
        TargetAnimation = targetAnimation;
        SenderAnimation = senderAnimation;
        SoundEffect = soundEffect;
        IsRanged = isRanged;
        ExecutionTime = executionTime;
    }
}
