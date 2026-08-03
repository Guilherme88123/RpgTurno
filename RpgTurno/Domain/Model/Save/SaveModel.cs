using Domain.Model.Base;

namespace Domain.Model.Save;

public class SaveModel : BaseModel
{
    public double Progress { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime LastPlayDate { get; set; }
}
