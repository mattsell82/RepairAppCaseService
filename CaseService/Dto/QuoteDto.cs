using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CaseService.Dto
{
    [DataContract]
    public class QuoteDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CaseId { get; set; }
        [DataMember]
        public DateTime Created { get; set; }
        [DataMember]
        public double Cost { get; set; }
        [DataMember]
        public bool Answered { get; set; }
        [DataMember]
        public bool Accepted { get; set; }
        [DataMember]
        public string Measure { get; set; }
    }
}