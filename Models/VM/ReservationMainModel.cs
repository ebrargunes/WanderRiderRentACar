using System.ComponentModel.DataAnnotations;

public class ReservationMainModel
{
    public int SelCarId { get; set; }
    public UserVM User { get; set; }
    public CarVM Car { get; set; }=null;
    public DateOnly PickupDate { get; set; }
    public DateOnly ReturnDate { get; set; }
           
    // Kiralama Sözleşmesi ve Bilgilendirme Formu kabulü
    [Required(ErrorMessage = "Kiralama sözleşmesini kabul etmeniz gerekmektedir.")]
    public bool AcceptedAgreement { get; set; }

    // Kampanya bildirimlerini kabul etme
    [Required(ErrorMessage = "Kampanya bildirimlerini kabul etmeniz gerekmektedir.")]
    public bool AcceptedNotifications { get; set; }
    public BreadCrumbModel BreadCrumb { get; set; }=null;
}