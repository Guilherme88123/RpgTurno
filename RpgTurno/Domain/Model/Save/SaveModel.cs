using Domain.Enum.Save;
using Domain.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model.Save;

public class SaveModel : BaseModel
{
    public SavePositionType Position { get; set; }

    public double Progress { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime LastPlayDate { get; set; }

    [NotMapped]
    public bool HasGameFinish => Progress >= 100;
}
