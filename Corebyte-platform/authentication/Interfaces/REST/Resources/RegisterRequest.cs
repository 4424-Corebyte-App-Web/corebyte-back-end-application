using System.ComponentModel.DataAnnotations;

namespace Corebyte_platform.authentication.Interfaces.REST.Resources;

public class RegisterRequest
{
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "El correo electrónico es requerido")]
    [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "La contraseña es requerida")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; }
    
    [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; }
}