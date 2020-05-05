using OnlineShop.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace OnlineShop.Models.ViewModels.Admins
{
    public class AdminVM
    {
        public AdminVM()
        {

        }

        public AdminVM(AdminDTO row)
        {
            Id = row.Id;
            Email = row.Email;
            Name = row.Name;
            Surname = row.Surname;
            Role = row.Role;
            Phone = row.Phone;
            Password = row.Password;
        }

        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}