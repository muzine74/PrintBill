using AutoMapper;
using DataBridge.Entity;
using DataBridge.Helpers;
using DBConnection;
using DBConnection.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBridge
{
    public class EmployeeManipulation
    {
        DbContextOptions<RamssisCleaningContex> options;
        RamssisCleaningContex ramssisCleaningContex;
        MapperConfiguration config;
        IMapper _mapper;

        Employee employee;
        EmployeePoco employeePoco;

        public EmployeeManipulation()
        {
            employee = new Employee();
            options = new DbContextOptionsBuilder<RamssisCleaningContex>()
            .UseSqlServer("Server=DESKTOP-71ON71H\\SQLEXPRESS;Database=RamssisCleaningDB;Trusted_Connection=True;TrustServerCertificate=true;")
            .Options;

            ramssisCleaningContex = new RamssisCleaningContex(options);

            EmployeeEmployeePcoMapper();

        }

        private void EmployeeEmployeePcoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeePoco>()
                    // Propriétés simples
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                    .ForMember(dest => dest.NAS, opt => opt.MapFrom(src => src.NAS))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.name))
                    .ForMember(dest => dest.EmployeeMail, opt => opt.MapFrom(src => src.EmployeeMail))
                    .ForMember(dest => dest.EmployeePhone, opt => opt.MapFrom(src => src.EmployeePhone))
                    .ForMember(dest => dest.EmployeeNote, opt => opt.MapFrom(src => src.notes))

                    // Adresse (prend la première adresse de la collection)
                    .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().AddressId))
                    .ForMember(dest => dest.EmployeeCivicNumber, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().civicNumber))
                    .ForMember(dest => dest.EmployeeSuite, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().suite))
                    .ForMember(dest => dest.EmployeeZipCode, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().zipCode))
                    .ForMember(dest => dest.EmployeeCity, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().city))
                    .ForMember(dest => dest.EmployeeState, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().state))
                    .ForMember(dest => dest.EmployeeCountry, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().country))
                    .ForMember(dest => dest.EmployeeAdressNote, opt => opt.MapFrom(src =>
                        src.Addresses.FirstOrDefault().notes))

                    // Compagnies (mapping de EmployeeCompanies vers CompagniePoco)
                    .ForMember(dest => dest.EmployeeCompagnies, opt => opt.MapFrom(src =>
                        src.EmployeeCompanies.Select(ec => ec.Company)));

                // Mapping pour Company vers CompagniePoco (si nécessaire)
                
            });

            _mapper = config.CreateMapper();
        }

        /*  public void SaveCreateEmployee(EmployeePoco emplPoco)
          {

               Employee employee = new Employee();

              employee.EmployeeId = Guid.NewGuid();
              employee.name = emplPoco.EmployeeName;
              employee.mail = emplPoco.EmployeeMail;
              employee.phone = emplPoco.EmployeePhone;
              employee.notes = emplPoco.EmployeeNote;


              Address address = new Address();

              address.AddressId = Guid.NewGuid();
              address.civicNumber = emplPoco.EmployeeCivicNumber;
              address.suite = emplPoco.EmployeeSuite;
              address.city = emplPoco.EmployeeCity;
              address.state = emplPoco.EmployeeState;
              address.country = emplPoco.EmployeeCountry;
              address.zipCode = emplPoco.EmployeeZipCode;
              address.notes = emplPoco.EmployeeAdressNote;

              EmployeeAddress employeeAddress = new EmployeeAddress();

              employeeAddress.AddressId = address.AddressId;
              employeeAddress.EmployeeId = employee.EmployeeId;

              // reste relation employee compagny a la mettre a jours

              List<EmployeeCompany> companyEmployeeList = new List<EmployeeCompany>();

              foreach (var item in emplPoco.EmployeeCompagnies)
              {
                  EmployeeCompany companyEmployee = new EmployeeCompany();

                  companyEmployee.EmployeeId = employee.EmployeeId;
                  companyEmployee.CompanyId = (Guid)item.CompagnieID;
                  companyEmployeeList.Add(companyEmployee);
              }


              ramssisCleaningContex.Employees.Add(employee);
              ramssisCleaningContex.EmployeeAddresses.Add(employeeAddress);
              ramssisCleaningContex.Addresses.Add(address);

              ramssisCleaningContex.EmployeeCompanies.AddRange(companyEmployeeList);

              ramssisCleaningContex.SaveChanges();

          }*/


        public void SaveCreateEmployee(EmployeePoco emplPoco)
        {
            if (emplPoco == null)
                throw new ArgumentNullException(nameof(emplPoco));

            Guid employeeId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();

            var employee = new Employee
            {
                EmployeeId = employeeId,
                NAS = emplPoco.NAS,
                name = emplPoco.EmployeeName,
                EmployeeMail = emplPoco.EmployeeMail,
                EmployeePhone = emplPoco.EmployeePhone,
                notes = emplPoco.EmployeeNote
            };

            var address = new Address
            {
                AddressId = addressId,
                civicNumber = emplPoco.EmployeeCivicNumber,
                suite = emplPoco.EmployeeSuite,
                city = emplPoco.EmployeeCity,
                state = emplPoco.EmployeeState,
                country = emplPoco.EmployeeCountry,
                zipCode = emplPoco.EmployeeZipCode,
                notes = emplPoco.EmployeeAdressNote
            };

            var employeeAddress = new EmployeeAddress
            {
                EmployeeId = employeeId,
                AddressId = addressId,
                Employee = employee,
                Address = address
            };

            var employeeCompanies = emplPoco.EmployeeCompagnies
                .Select(c => new EmployeeCompany
                {
                    EmployeeId = employeeId,
                    CompanyId = (Guid)c.CompagnieID
                })
                .ToList();

            ramssisCleaningContex.Employees.Add(employee);
            ramssisCleaningContex.Addresses.Add(address);
            ramssisCleaningContex.EmployeeAddresses.Add(employeeAddress);
            ramssisCleaningContex.EmployeeCompanies.AddRange(employeeCompanies);

            ramssisCleaningContex.SaveChanges();
        }

        public List<Employee> GetEmployee()
        {
            return ramssisCleaningContex.Employees.ToList();
        }

        public EmployeePoco GetEmployeeById(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                employeeId = "00000000-0000-0000-0000-000000000000";


            return ramssisCleaningContex.EmployeeAddresses
                .AsNoTracking()
                .Where(ea => ea.Employee.EmployeeId == new Guid(employeeId))
                .Select(ea => new EmployeePoco
                {
                    EmployeeId = ea.Employee.EmployeeId,
                    NAS = ea.Employee.NAS,
                    EmployeeName = ea.Employee.name,
                    EmployeeMail = ea.Employee.EmployeeMail,
                    EmployeePhone = ea.Employee.EmployeePhone,
                    EmployeeNote = ea.Employee.notes,

                    AddressId = ea.Address.AddressId,
                    EmployeeCivicNumber = ea.Address.civicNumber,
                    EmployeeSuite = ea.Address.suite,
                    EmployeeZipCode = ea.Address.zipCode,
                    EmployeeCity = ea.Address.city,
                    EmployeeState = ea.Address.state,
                    EmployeeCountry = ea.Address.country,
                    EmployeeAdressNote = ea.Address.notes,

                    EmployeeCompagnies = ea.Employee.EmployeeCompanies
                        .Select(ec => new CompagniePoco
                        {
                            CompagnieID = ec.Company.CompanyId,
                            CompagnieName = ec.Company.companyName
                        })
                        .ToList()
                })
                .FirstOrDefault();
        }


        public List<EmployeePoco> GetAllEmplyee()
        {

            return ramssisCleaningContex.EmployeeAddresses
              .AsNoTracking()
              .Select(ea => new EmployeePoco
              {
                  EmployeeId = ea.Employee.EmployeeId,
                  NAS = ea.Employee.NAS,
                  EmployeeName = ea.Employee.name,
                  EmployeeMail = ea.Employee.EmployeeMail,
                  EmployeePhone = ea.Employee.EmployeePhone,
                  EmployeeNote = ea.Employee.notes,

                  AddressId = ea.Address.AddressId,
                  EmployeeCivicNumber = ea.Address.civicNumber,
                  EmployeeSuite = ea.Address.suite,
                  EmployeeZipCode = ea.Address.zipCode,
                  EmployeeCity = ea.Address.city,
                  EmployeeState = ea.Address.state,
                  EmployeeCountry = ea.Address.country,
                  EmployeeAdressNote = ea.Address.notes,

                  EmployeeCompagnies = ea.Employee.EmployeeCompanies
                        .Select(ec => new CompagniePoco
                        {
                            CompagnieID = ec.Company.CompanyId,
                            CompagnieName = ec.Company.companyName
                        })
                        .ToList()
              })
              .OrderBy(x => x.EmployeeName)
              .ToList();
        }


        public List<EmployeePoco> GetEmployeeByNas(string nas)
        {

            return ramssisCleaningContex.EmployeeAddresses
              .AsNoTracking()
               .Where(ea => ea.Employee.NAS == nas)
              .Select(ea => new EmployeePoco
              {
                  EmployeeId = ea.Employee.EmployeeId,
                  NAS = ea.Employee.NAS,
                  EmployeeName = ea.Employee.name,
                  EmployeeMail = ea.Employee.EmployeeMail,
                  EmployeePhone = ea.Employee.EmployeePhone,
                  EmployeeNote = ea.Employee.notes,

                  AddressId = ea.Address.AddressId,
                  EmployeeCivicNumber = ea.Address.civicNumber,
                  EmployeeSuite = ea.Address.suite,
                  EmployeeZipCode = ea.Address.zipCode,
                  EmployeeCity = ea.Address.city,
                  EmployeeState = ea.Address.state,
                  EmployeeCountry = ea.Address.country,
                  EmployeeAdressNote = ea.Address.notes,

                  EmployeeCompagnies = ea.Employee.EmployeeCompanies
                        .Select(ec => new CompagniePoco
                        {
                            CompagnieID = ec.Company.CompanyId,
                            CompagnieName = ec.Company.companyName
                        })
                        .ToList()
              })
              .OrderBy(x => x.EmployeeName)
              .ToList();
        }

        private RamssisCleaningContex CreateContext()
        {
            return new RamssisCleaningContex(options);
        }
        public void UpdateEmployee(EmployeePoco emplPoco)
        {
            using var context = CreateContext();
            using var transaction = context.Database.BeginTransaction();

            var employee = context.Employees
                .FirstOrDefault(e => e.EmployeeId == emplPoco.EmployeeId);

            if (employee == null)
                throw new InvalidOperationException("Employee introuvable.");

            employee.NAS = emplPoco.NAS;
            employee.name = emplPoco.EmployeeName;
            employee.EmployeeMail = emplPoco.EmployeeMail;
            employee.EmployeePhone = emplPoco.EmployeePhone;
            employee.notes = emplPoco.EmployeeNote;

            var address = context.Addresses
                .FirstOrDefault(a => a.AddressId == emplPoco.AddressId);

            if (address == null)
                throw new InvalidOperationException("Adresse introuvable.");

            address.civicNumber = emplPoco.EmployeeCivicNumber;
            address.suite = emplPoco.EmployeeSuite;
            address.city = emplPoco.EmployeeCity;
            address.state = emplPoco.EmployeeState;
            address.country = emplPoco.EmployeeCountry;
            address.zipCode = emplPoco.EmployeeZipCode;
            address.notes = emplPoco.EmployeeAdressNote;

            var existingCompanies = context.EmployeeCompanies
                .Where(ec => ec.EmployeeId == emplPoco.EmployeeId)
                .ToList();

            var newCompanyIds = emplPoco.EmployeeCompagnies
                    .Select(c => c.CompagnieID) // Removed '.Value' as CompagnieID is already of type Guid
                .ToList();

            context.EmployeeCompanies.RemoveRange(
                existingCompanies.Where(ec => !newCompanyIds.Contains(ec.CompanyId))
            );

            context.EmployeeCompanies.AddRange(
                newCompanyIds
                    .Except(existingCompanies.Select(ec => ec.CompanyId))
                    .Select(id => new EmployeeCompany
                    {
                        EmployeeId = emplPoco.EmployeeId,
                        CompanyId = id
                    })
            );

            context.SaveChanges();
            transaction.Commit();
        }

        public EmployeePoco GetEmployeeByCredential(Login login)
        {
            //EmployeePoco employeePoco = new EmployeePoco();

            Employee employee = ramssisCleaningContex.EmployeeCredentials
                             .Where(cred => cred.Username == login.Username && cred.PasswordHash == login.Password)
                             .Join(ramssisCleaningContex.Employees,
                                 cred => cred.EmplyeeID,
                                 emp => emp.EmployeeId,
                                 (cred, emp) => emp)
                             .FirstOrDefault();  // ou SingleOrDefault() si unique

            if (employee == null)
            {
                return null;
            }

            return  _mapper.Map<EmployeePoco>(employee);
        }

        

    }
}
