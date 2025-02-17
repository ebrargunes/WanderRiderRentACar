using System.ComponentModel.DataAnnotations;

public class AdminAddCarModel
{
    public List<TransmissionVM> Transmissions { get; set; }
    public List<FuelTypeVM> FuelTypes { get; set; }
    public List<SegmentVM> Segments { get; set; }
    public List<RentStoreVM> RentStores { get; set; }

    public int CarId { get; set; }

    [Required(ErrorMessage = "Araç adı zorunludur.")]
    public string CarName { get; set; } = null!;

    [Required(ErrorMessage = "Yakıt türü seçiniz!")]
    public int SelFuelTypeId { get; set; }

    [Required(ErrorMessage = "Model Giriniz!")]
    public int Model { get; set; }

    [Required(ErrorMessage = "Vites türü seçiniz!")]
    public int SelTransmissionId { get; set; }

    [Required(ErrorMessage = "Segment türü seçiniz!")]
    public int SelSegmentId { get; set; }

    [Required(ErrorMessage = "Kayıt edilecek ofis seçiniz!")]
    public int SelRentStoreId { get; set; }
    public bool IsAvailable { get; set; }

    [Required(ErrorMessage = "Resim url'si giriniz")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Depozito Ucreti Giriniz")]
    public decimal DepositPrice { get; set; }
}