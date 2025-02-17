using System;
using System.Collections.Generic;

namespace WanderRiderRentACar.DMO;

public partial class Sale
{
    public int SalesId { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public DateOnly PickupDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
