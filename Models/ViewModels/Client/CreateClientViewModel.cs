using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models.ViewModels.Client;

public class CreateClientViewModel
{
    [Required(ErrorMessage = "The 'Name' field is mandatory")]
    public string? Name{get;set;}
     
    [Required(ErrorMessage = "The 'Login' field is mandatory")]
    public string? Login {get;set;}
    
    [Required(ErrorMessage = "The 'Password' field is mandatory")]
    [DataType(DataType.Password)]
    public string? Password{get;set;}
    
    [Required(ErrorMessage = "The 'RepeatPassword' field is mandatory")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords must match")]
    public string? RepeatPassword{get;set;}
}