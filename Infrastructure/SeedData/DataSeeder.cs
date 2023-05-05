using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public class DataSeeder
    {
        #region Injection

        private readonly CarRentalContext _context;

        public DataSeeder(CarRentalContext context)
        {
            _context = context;
        }

        #endregion Injection

        public void SeedData()
        {
            SeedUsers();
            SeedCars();
        }

        private void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                _context.Users.AddRange(
                    new User { Name = "John", Surname = "Grece", Email = "ggrece0@wiley.com", PhoneNumber = "297-176-0374" },
                    new User { Name = "Agathe", Surname = "McMaster", Email = null, PhoneNumber = "687-453-8992" },
                    new User { Name = "Alfie", Surname = "Leving", Email = "alevingm@wordpress.com", PhoneNumber = "908-118-0416" },
                    new User { Name = "Denise", Surname = "O'Gorman", Email = null, PhoneNumber = "297-176-0374" },
                    new User { Name = "Camila", Surname = "Cowlard", Email = "ccowlard1b@shutterfly.com", PhoneNumber = "599-492-4167" }
                );
                _context.SaveChanges();
            }
        }

        private void SeedCars()
        {
            if (!_context.Cars.Any())
            {
                _context.Cars.AddRange(
                    new Car { Make = "Lotus", Model = "Esprit", RegistrationNumber = "1FTEX1C87A" },
                    new Car { Make = "Chrysler", Model = "LHS", RegistrationNumber = "WAUSH78E67" },
                    new Car { Make = "Ford", Model = "Focus", RegistrationNumber = "WA1VMAFEXA" },
                    new Car { Make = "Lexus", Model = "GS", RegistrationNumber = "TRURD38JX8" },
                    new Car { Make = "Mitsubishi", Model = "Lancer", RegistrationNumber = "1FTEX1C87A" },
                    new Car { Make = "GMC", Model = "Yukon XL 1500", RegistrationNumber = "SCFEBBAC0A" },
                    new Car { Make = "Mercedes-Benz", Model = "SL-Class", RegistrationNumber = "WAUYGAFCXC" },
                    new Car { Make = "Kia", Model = "Sportage", RegistrationNumber = "1D7RE3GK7B" },
                    new Car { Make = "Volkswagen", Model = "Passat", RegistrationNumber = "2G4GR5EK9C" },
                    new Car { Make = "Suzuki", Model = "Daewoo Lacetti", RegistrationNumber = "1N6AF0LX8E" }
                );
                _context.SaveChanges();
            }
        }
    }
}
