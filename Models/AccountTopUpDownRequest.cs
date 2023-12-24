namespace Models;

public class AccountTopUpDownRequest
{
    public Guid UserId { get; set; }
    public float Money { get; set; }
}