using CaseService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseService.Model
{
    public static class Map
    {
        public static CustomerDto CustomerToDto(Customer customer) {



            CustomerDto customerDto = new CustomerDto()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Zip = customer.Zip,
                City = customer.City
            };

            return customerDto;
        }

        public static StatusDto StatusToDto(Status status) {

            StatusDto statusDto = new StatusDto { Id = status.Id, Name = status.Name };

            return statusDto;
        }

        public static Quote DtoToQuote(QuoteDto quoteDto) {

            Quote quote = new Quote { 
                CaseId = quoteDto.CaseId,
                Accepted = quoteDto.Accepted, 
                Answered = quoteDto.Answered, 
                Cost = quoteDto.Cost, 
                Measure = quoteDto.Measure,
                DateTime = quoteDto.Created };

            return quote;
        }

        public static QuoteDto ToQuoteDto(Quote quote) {

            QuoteDto quoteDto = new QuoteDto {
                Id = quote.Id,
                CaseId = quote.CaseId,
                Created = quote.DateTime, 
                Measure = quote.Measure, 
                Accepted = quote.Accepted, 
                Answered = quote.Answered, 
                Cost = quote.Cost };

            return quoteDto;
        }
    }
}