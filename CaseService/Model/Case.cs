using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseService.Model
{
    [Table("Cases")]
    public class Case
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ErrorDescription { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public Guid Guid { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        public Case()
        {
            this.DateTime = DateTime.Now;
            this.Guid = Guid.NewGuid();
        }
    }
}