using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CaseService.Dto
{
    [DataContract]
    public class CaseDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int EmployeeId { get; set; }

        [DataMember]
        public DateTime DateTime { get; set; }

        [DataMember]
        public CustomerDto CustomerDto { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ErrorDescription { get; set; }

        [DataMember]
        public StatusDto StatusDto { get; set; }

        [DataMember]
        public Guid Guid { get; set; }

        [DataMember]
        public DateTime EstimatedDeliveryDate { get; set; }

    }
}