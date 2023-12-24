using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Account : BaseModel
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public float Money { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}