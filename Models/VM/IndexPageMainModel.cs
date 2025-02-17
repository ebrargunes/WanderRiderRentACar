public class IndexPageMainModel
{
    public List<RentStoreVM> RentStores { get; set; } //düzelt
    public int SelectedOfficeId { get; set; }
    public DateOnly PickupDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly ReturnDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

    public List<CarVM> AvailableCars { get; set; } =new List<CarVM>();
}


/*
{
    RentStores : [
        {
            RentStoresId : 1,
            RentStoresName : "Ankara"
        },
        {
            RentStoresId : 2,
            RentStoresName : "İstanbul"
        }
    ]
    SelectedOfficeId : 1,
    PickupDate : "2021-09-01",
    ReturnDate : "2021-09-02"
}
*/