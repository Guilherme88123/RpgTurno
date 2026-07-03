using Domain.Model.Texture.Sprite;

namespace Domain.Model.Skill.Base.Animation;

public record SkillAnimation
{
    public AnimationClip TargetAnimation { get; set; }
    public AnimationClip SenderAnimation { get; set; }
    public bool IsRanged { get; set; }
    public float ExecutionTime { get; set; }

    public SkillAnimation(AnimationClip targetAnimation, AnimationClip senderAnimation, bool isRanged, float executionTime)
    {
        TargetAnimation = targetAnimation;
        SenderAnimation = senderAnimation;
        IsRanged = isRanged;
        ExecutionTime = executionTime;
    }
}
