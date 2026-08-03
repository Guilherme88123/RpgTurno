using Domain.Enum.Stage;
using Domain.Model.Base;
using Domain.Model.Save;

namespace Domain.Model.Stage;

public class StageModel : BaseModel
{
    public int SaveId { get; set; }
    public SaveModel Save { get; set; }

    public StageCode StageCode { get; set; }
    public bool IsCompleted { get; set; }
}
