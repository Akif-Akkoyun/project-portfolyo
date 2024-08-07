﻿using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Mvc.Models
{
    public class RenewPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required,MinLength(1)]
        public string Token { get; set; } = string.Empty;
        [Required,MinLength(2)]
        public string Password { get; set; } = string.Empty;
        [Required,MinLength(2),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
