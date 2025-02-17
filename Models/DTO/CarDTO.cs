public class CarDTO
{
    public int CarId { get; set; }

    public string CarName { get; set; } = null!;

    public int FuelTypeId { get; set; }
    public string FuelTypeName { get; set; } = null!;

    public int Model { get; set; }

    public int TransmissionId { get; set; }
    public string TransmissionName { get; set; } = null!;

    public int SegmentId { get; set; }

    public string SegmentName { get; set; } = null!;

    public int? RentStoresId { get; set; }
    public string RentStoresName { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public string? ImageUrl { get; set; }
    public decimal DepositPrice { get; set; }
    public DateOnly PickupDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly ReturnDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
}