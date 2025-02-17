using System.ComponentModel.DataAnnotations;

public class LoginVM
{
    [Required(ErrorMessage = "Kullanıcı Adı zorunludur.")]
    public string UserName {get; set;}

    [Required(ErrorMessage = "Şifre zorunludur.")]
    public string Password {get; set;}

}
