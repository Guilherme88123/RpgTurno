using Domain.Model.Entity.Units.Base;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite;

namespace Domain.Model.Effect.Base;

public abstract class BaseEffect
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract SpriteData Icon { get; }

    public int Duration { get; private set; }

    public bool HasFinished => Duration <= 0;

    protected BaseEffect(int duration)
    {
        Duration = duration;
    }

    public virtual void OnApply(BaseUnitEntity unit) 
    { 

    }

    public virtual void OnTurnStart(BaseUnitEntity unit) 
    { 

    }

    public virtual void OnRemove(BaseUnitEntity unit) 
    { 

    }

    public virtual void OnAttack(SkillContext context) 
    { 

    }

    public virtual void OnReceiveAttack(SkillContext context) 
    { 

    }

    public void TickDuration()
    {
        if (Duration > 0)
            Duration--;
    }
}
