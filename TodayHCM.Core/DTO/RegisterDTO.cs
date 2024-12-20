﻿namespace TodayHCM.Core.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "Person Name can't be blank")]
    public string PersonName { get; set; } = string.Empty;


    [Required(ErrorMessage = "Email can't be blank")]
    [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
    [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already is use")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "Phone number can't be blank")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain digits only")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;


    [Required(ErrorMessage = "Password can't be blank")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;


    [Required(ErrorMessage = "Confirm Password can't be blank")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
