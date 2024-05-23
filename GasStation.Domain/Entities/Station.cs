using GasStation.Domain.Enums;

namespace GasStation.Domain.Entities;

public class Station : Base
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public float Price { get; set; }
    public StationType StationType { get; set; } = StationType.Gas;
}
