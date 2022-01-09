using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaseService.Model
{
    [Table("Quotes")]
    public class Quote
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public Case Case { get; set; }
        public DateTime DateTime { get; set; }
        public string Measure { get; set; }
        public double Cost { get; set; }
        public bool Answered { get; set; }
        public bool Accepted { get; set; }
    }
}