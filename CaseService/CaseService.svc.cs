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
                Case @case = db.Cases.Find(id);

                if (@case is null)
                {
                    return;
                }

                db.Cases.Remove(@case);
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
                    var cases = db.Cases.ToList();


                    if (cases.Count > 0)
                    {
                        var caseDtos = cases.Select(c => new CaseDto 
                        { 
                            Id = c.Id, 
                            StatusId = c.StatusId, 
                            CustomerId = c.CustomerId, 
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
                CustomerId = caseDto.CustomerId,
                DateTime = DateTime.Now,
                EmployeeId = 1,
                ProductId = 1,
                StatusId = 1,
                ErrorDescription = caseDto.ErrorDescription,
                Guid = Guid.NewGuid()
            };

            try
            {
                using (CaseDbContext db = new CaseDbContext())
                {
                    newCase.Customer = db.Customers.Find(caseDto.CustomerId);
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
                    var cases = db.Cases.ToList();


                    if (cases.Count > 0)
                    {
                        var caseDtos = cases.Select(c => new CaseDto 
                        { 
                            Id = c.Id, 
                            StatusDto = db.Status.Select(s => new StatusDto { Id = s.Id, Name = s.Name}).First(), 
                            CustomerId = c.CustomerId, 
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
                CustomerId = caseDto.CustomerId,
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
                    newCase.Customer = db.Customers.Find(caseDto.CustomerId);
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
                    StatusDto statusDto = new StatusDto { Id = status.Id, Name = status.Name };

                    CaseDto dto = new CaseDto { Id = row.Id, Guid = row.Guid, CustomerId = row.CustomerId, DateTime = row.DateTime, EmployeeId = row.EmployeeId, ErrorDescription = row.ErrorDescription, ProductId = row.ProductId, StatusDto = statusDto};

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
                    StatusDto statusDto = new StatusDto { Id = status.Id, Name = status.Name };

                    CaseDto dto = new CaseDto { Id = row.Id, Guid = row.Guid, CustomerId = row.CustomerId, DateTime = row.DateTime, EmployeeId = row.EmployeeId, ErrorDescription = row.ErrorDescription, ProductId = row.ProductId, StatusDto = statusDto };

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
    }
}
