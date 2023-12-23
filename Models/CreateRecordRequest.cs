namespace Models;

public class CreateRecordRequest
{
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public string MoneySpent { get; set; }
}