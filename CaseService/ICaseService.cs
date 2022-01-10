﻿using CaseService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICaseService
    {

        [OperationContract]
        CustomerDto GetCustomer(int value);

        [OperationContract]
        List<CustomerDto> GetCustomers();

        [OperationContract]
        void DeleteCustomer(int id);

        [OperationContract]
        int CreateCustomer(CustomerDto customerDto);

        [OperationContract]
        void EditCustomer(CustomerDto customerDto);

        [OperationContract]
        List<StatusDto> GetStatusList();

        [OperationContract]
        List<CaseDto> GetCases();

        [OperationContract]
        void CreateCase(CaseDto caseDto);

        [OperationContract]
        CaseDto GetCaseByGuid(Guid guid);

        [OperationContract]
        CaseDto GetCase(int id);

        [OperationContract]
        void DeleteCase(int id);

        [OperationContract]
        void AddQuote(QuoteDto quoteDto);

        [OperationContract]
        void AnswerQuote(int id, bool answer);

        [OperationContract]
        void DeleteQuote(int id);

        [OperationContract]
        void EditCase(CaseDto caseDto);
    }
}
