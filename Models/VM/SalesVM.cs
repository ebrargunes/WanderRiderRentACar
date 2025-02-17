public partial class SalesVM
{
    public int SalesId { get; set; }

    public int UserId { get; set; }

    public int CarId { get; set; }

    public DateOnly PickupDate { get; set; }  

    public DateOnly ReturnDate { get; set; }

}