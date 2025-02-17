public class CarsPageMainModel
{
    public int SelCarId { get; set; }
    public List<TransmissionVM> Transmissions { get; set; }
    public List<FuelTypeVM> FuelTypes { get; set; }
    public List<SegmentVM> Segments { get; set; }
    public int SelFuelTypeId { get; set; }
    public int SelSegmentId { get; set; }
    public int SelTransmissionId { get; set; }
    public List<CarVM> Cars { get; set; }
    public BreadCrumbModel BreadCrumb { get; set; }

}