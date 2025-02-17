using System.ComponentModel.DataAnnotations;

public class UserVM
{
    public int UserId { get; set; }
    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Kullanıcı soyadı zorunludur.")]
    public string UserSurname { get; set; } = null!;

    [Required(ErrorMessage = "Email adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçersiz email adresi.")]
    public string UserEmail { get; set; } = null!;

    [Required(ErrorMessage = "Adres zorunludur.")]
    public string UserAddress { get; set; } = null!;

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
    public string UserPhone { get; set; } = null!;

    [Required(ErrorMessage = "Ehliyet numarası zorunludur.")]
    [Range(100000, 999999, ErrorMessage = "Ehliyet numarası 6 haneli olmalıdır.")]
    public string LicenceNo { get; set; } = null!;

}

