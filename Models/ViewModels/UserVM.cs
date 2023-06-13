using Extensions.ValidateAttribute;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels;

public class UserVM
{
    [Required]
    public string uName { get; set; }

    [Required]
    public int uAge { get; set; }

    [Required, MinLength(1)]
    public string Password { get; set; }
}
