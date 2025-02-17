using System;
using System.Collections.Generic;

namespace WanderRiderRentACar.DMO;

public partial class Transmission
{
    public int TransmissionId { get; set; }

    public string TransmissionName { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
