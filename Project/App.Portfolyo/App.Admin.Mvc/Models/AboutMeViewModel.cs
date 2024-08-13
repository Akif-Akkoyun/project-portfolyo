﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class AboutMeViewModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Introduction { get; set; } = default!;
        [Required]
        public string ImageUrl1 { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string DateOfbirth { get; set; } = default!;
        [Required]
        public string Address { get; set; } = default!;
        [Required,EmailAddress]
        public string Email { get; set; } = default!;
        [Required,Phone]
        public string PhoneNumber { get; set; } = default!;
        [Required]
        public int ZipCode { get; set; }
        public DateTime CreatedAt { get; set; } = default!;
    }
}
