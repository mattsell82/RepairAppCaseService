﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseService.Model
{
    [Table("Customers")]
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

#nullable enable
        [StringLength(50)]
        public string? LastName { get; set; }
#nullable disable

        [StringLength(200)]
        [Required]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        public string Phone { get; set; }

#nullable enable
        [StringLength(50)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? Zip { get; set; }

        [StringLength(50)]
        public string? City { get; set; }
#nullable disable
        public List<Case> Cases { get; set; }
    }
}