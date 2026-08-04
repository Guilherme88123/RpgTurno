using Domain.Enum.Unit;
using Domain.Model.Base;
using Domain.Model.Save;

namespace Domain.Model.Unit;

public class UnitModel : BaseModel
{
    public Guid SaveId { get; set; }
    public SaveModel Save { get; set; }

    public UnitCode UnitCode { get; set; }

    public int Level { get; set; }
    public int CurrentExperience { get; set; }
}
