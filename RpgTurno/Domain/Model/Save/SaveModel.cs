using Domain.Enum.Save;
using Domain.Model.Base;

namespace Domain.Model.Save;

public class SaveModel : BaseModel
{
    public SavePositionType Position { get; set; }

    public double Progress { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime LastPlayDate { get; set; }
}
