using System;
using System.Collections.Generic;

namespace WanderRiderRentACar.DMO;

public partial class RentStore
{
    public int RentStoresId { get; set; }

    public string RentStoresName { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
