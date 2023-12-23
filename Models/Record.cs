using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Record : BaseModel
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    [Required]
    public float MoneySpent { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
    [JsonIgnore]
    public virtual Category? Category { get; set; }
    
    public Record(Guid id, Guid userId, Guid categoryId, string moneySpent)
    {
        Id = id;
        UserId = userId;
        CategoryId = categoryId;
        CreatedAt = DateTime.Now.ToUniversalTime();
        MoneySpent = float.Parse(moneySpent);
    }

    public Record()
    {
        
    }
}