using System;
using System.Collections.Generic;

namespace WanderRiderRentACar.DMO;

public partial class Segment
{
    public int SegmentId { get; set; }

    public string SegmentName { get; set; } = null!;

    public decimal DepositPrice { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
