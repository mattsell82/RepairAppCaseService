using CaseService.Dto;
using CaseService.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CaseService : ICaseService
    {
        public CustomerDto GetCustomer(int id)
        {
            Customer customer = new Customer();

            using (CaseDbContext db = new CaseDbContext()) 
            {
                customer = db.Customers.Find(id);
            }

            if (customer is null)
            {
                return null;
            }

            CustomerDto customerDto = Map.CustomerToDto(customer);
         
            return customerDto;
        }

        public List<CustomerDto> GetCustomers()
        {
            using (CaseDbContext db = new CaseDbContext())
            {
                var customers = db.Customers.ToList();

                var customerDtos = customers.Select(customer => new CustomerDto()
                { 
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    Zip = customer.Zip,
                    City = customer.City
                }).ToList();

                return customerDtos;
            }

        }

        public void DeleteCustomer(int id) {

            using (CaseDbContext db = new CaseDbContext())
            {
                var customer = db.Customers.Find(id);

                if (customer is null)
                {
                    return;
                }

                db.Customers.Remove(customer);
                db.SaveChanges();
            }
        }

        public void DeleteCase(int id)
        {
            using (CaseDbContext db = new CaseDbContext())
            {
                Case test = db.Cases.Find(id);

                if (test is null)
                {
                    return;
                }

                db.Cases.Remove(test);
                db.SaveChanges();
            }
        }

        public int CreateCustomer(CustomerDto dto) {

            if (dto is null)
            {
                return -1;
            }

            Customer customer = new Customer()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                Zip = dto.Zip,
                City = dto.City
            };

            using (CaseDbContext db = new CaseDbContext())
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                return customer.Id;
            }

        }

        public void EditCustomer(CustomerDto dto)
        {
            if (dto is null)
            {
                return;
            }

            using (CaseDbContext db = new CaseDbContext())
            {
                Customer customer = db.Customers.Find(dto.Id);

                customer.FirstName = dto.FirstName;
                customer.LastName = dto.LastName;
                customer.Phone = dto.Phone;
                customer.Address = dto.Address;
                customer.Zip = dto.Zip;
                customer.City = dto.City;
                customer.Email = dto.Email;


                db.SaveChanges();
            }
        }

        public List<StatusDto> GetStatusList()
        {
            using (CaseDbContext db = new CaseDbContext())
            {
                var statuslist = db.Status.ToList();

                var statusDtoList = statuslist.Select(s => new StatusDto { Id = s.Id, Name = s.Name }).ToList();

                return statusDtoList;
            };
        }

        public List<CaseDto> GetCases()
        {
            try
            {
                using (CaseDbContext db = new CaseDbContext())
                {
                    //var cases = db.Cases.ToList();

                    List<Case> cases = db.Cases.Include("Customer").Include("Status").ToList();



                    if (cases.Count > 0)
                    {
                        var caseDtos = cases.Select(c => new CaseDto 
                        { 
                            Id = c.Id, 
                            StatusDto = Map.StatusToDto(c.Status), 
                            CustomerDto = Map.CustomerToDto(c.Customer),
                            DateTime = c.DateTime, 
                            EmployeeId = c.EmployeeId, 
                            ErrorDescription = c.ErrorDescription, 
                            Guid = c.Guid, 
                            ProductId = c.ProductId })
                            .ToList();
                        return caseDtos;
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Felmeddelanded GetCases: " + e.Message);
                Debug.WriteLine("StackTrace GetCases: " + e.StackTrace);
                throw;
            }


        }

        public void CreateCase(CaseDto caseDto)
        {
            Case newCase = new Case
            {
                CustomerId = caseDto.CustomerDto.Id,
                DateTime = DateTime.Now,
                EmployeeId = 1,
                ProductId = caseDto.ProductId,
                StatusId = 1,
                ErrorDescription = caseDto.ErrorDescription,
                Guid = Guid.NewGuid()
            };

            try
            {
                using (CaseDbContext db = new CaseDbContext())
                {
                    newCase.Customer = db.Customers.Find(caseDto.CustomerDto.Id);
                    newCase.Status = db.Status.Find(newCase.StatusId);

                    db.Cases.Add(newCase);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("Felmeddelande CreateCase: " + e.Message);
                throw;
            }


        }

        public CaseDto GetCaseByGuid(Guid guid)
        {
            try
            {
                using (CaseDbContext db = new CaseDbContext())
                {
                    Case row = db.Cases.First(c => c.Guid == guid);

                    Status status = db.Status.Find(row.StatusId);
                    StatusDto statusDto = Map.StatusToDto(status);

                    Customer customer = db.Customers.Find(row.CustomerId);
                    CustomerDto customerDto = Map.CustomerToDto(customer);

                    List<Quote> quotes = db.Quotes.Where(q => q.CaseId == row.Id).ToList();
                    List<QuoteDto> quoteDtos = quotes.Select(q => Map.ToQuoteDto(q)).ToList();

                    CaseDto dto = new CaseDto { Id = row.Id, Guid = row.Guid, CustomerDto = customerDto, DateTime = row.DateTime, EmployeeId = row.EmployeeId, ErrorDescription = row.ErrorDescription, ProductId = row.ProductId, StatusDto = statusDto, QuoteDtos = quoteDtos};

                    return dto;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("Fel GetCasesByGuid: " + e.Message);
                Debug.WriteLine("Fel GetCasesByGuid: " + e.StackTrace);
                throw;
            }

            
        }

        public CaseDto GetCase(int id)
        {
            try
            {
                using (CaseDbContext db = new CaseDbContext())
                {
                    Case row = db.Cases.Find(id);

                    Status status = db.Status.Find(row.StatusId);
                    StatusDto statusDto = Map.StatusToDto(status);

                    Customer customer = db.Customers.Find(row.CustomerId);
                    CustomerDto customerDto = Map.CustomerToDto(customer);

                    List<Quote> quotes = db.Quotes.Where(q => q.CaseId == id).ToList();
                    List<QuoteDto> quoteDtos = quotes.Select(q => Map.ToQuoteDto(q)).ToList();

                    CaseDto dto = new CaseDto { Id = row.Id, Guid = row.Guid, CustomerDto = customerDto, DateTime = row.DateTime, EmployeeId = row.EmployeeId, ErrorDescription = row.ErrorDescription, ProductId = row.ProductId, StatusDto = statusDto, QuoteDtos = quoteDtos };

                    return dto;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("Fel GetCase: " + e.Message);
                Debug.WriteLine("Fel CetCase: " + e.StackTrace);
                throw;
            }
        }

        public void AddQuote(QuoteDto quoteDto) {

            Quote quote = new Quote
            {
                CaseId = quoteDto.CaseId,
                Accepted = false,
                Answered = false,
                Measure = quoteDto.Measure,
                Cost = quoteDto.Cost,
                DateTime = DateTime.Now
            };

            using (CaseDbContext db = new CaseDbContext())
            {
                db.Quotes.Add(quote);
                db.SaveChanges();
            }
        }

        public void AnswerQuote(int id, bool answer) {

            using (CaseDbContext db = new CaseDbContext())
            {
                Quote quote = db.Quotes.Find(id);
                quote.Answered = true;

                if (answer)
                {
                    quote.Accepted = true;
                }
                else
                {
                    quote.Accepted = false;
                }

                db.SaveChanges();
            }

        }

        public void DeleteQuote(int id) {
            using (CaseDbContext db = new CaseDbContext())
            {
                var quote = db.Quotes.Find(id);

                db.Quotes.Remove(quote);
                db.SaveChanges();
            }
        
        }
    }
}
