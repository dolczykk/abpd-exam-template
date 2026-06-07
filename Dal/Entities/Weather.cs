namespace Apbd.Dal.Entities;

public class Weather : BaseEntity
{
    public required string City { get; set; }
    public double Temperature { get; set; }
}
