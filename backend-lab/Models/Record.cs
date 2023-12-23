namespace backend_lab.Models;

public class Record
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public float MoneySpent { get; set; }

    public Record(Guid id, Guid userId, Guid categoryId, DateTime createdAt, string moneySpent)
    {
        Id = id;
        UserId = userId;
        CategoryId = categoryId;
        CreatedAt = createdAt;
        MoneySpent = float.Parse(moneySpent);
    }

    public Record()
    {
        
    }
}