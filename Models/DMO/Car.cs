using System;
using System.Collections.Generic;

namespace WanderRiderRentACar.DMO;

public partial class Car
{
    public int CarId { get; set; }

    public string CarName { get; set; } = null!;

    public int FuelTypeId { get; set; }

    public int Model { get; set; }

    public int TransmissionId { get; set; }

    public int SegmentId { get; set; }

    public bool IsAvailable { get; set; }

    public string? ImageUrl { get; set; }

    public int? RentStoresId { get; set; }

    public virtual FuelType FuelType { get; set; } = null!;

    public virtual RentStore? RentStores { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Segment Segment { get; set; } = null!;

    public virtual Transmission Transmission { get; set; } = null!;
}
