using CaseService.Dto;
using CaseService.Model;
using System;
using System.Collections.Generic;
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
    }
}
