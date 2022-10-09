using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VTB_Hakaton.DataAccessLayer.Models;

namespace WebApi.DataAccessLayer.Models
{
    public class User : IdentityUser<int>
    {
        public UserRole Role { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Surname { get; set; }

        [StringLength(20)]
        public string? MiddleName { get; set; }

        public string? PrivateKey { get; set; }
        public string? PublicKey { get; set; }

        public string? Phone { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<ShopItem> ShopItems { get; set; }

        public string FullName => Surname + " " + Name + (string.IsNullOrEmpty(MiddleName) ? string.Empty : " " + MiddleName);
        // Surname N. M.
        public string ShortName => Surname + " " + Name[0].ToString() + "." + (string.IsNullOrEmpty(MiddleName) ? string.Empty : " " + MiddleName[0].ToString() + ".");
    }
}
