using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
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
        private readonly IAuthenticationHelper _authenticationHelper;

        public DataSeeder(CarRentalContext context, IAuthenticationHelper authenticationHelper)
        {
            _context = context;
            _authenticationHelper = authenticationHelper;
        }

        #endregion Injection

        public void SeedData()
        {
            var userCredentials = SeedUserCredentials();
            var users = SeedUsers(userCredentials);
            var cars = SeedCars();
            SeedRentals(users, cars);
        }

        private UserCredentials[] SeedUserCredentials()
        {
            UserCredentials[] userCredentials = new UserCredentials[5];
            if (!_context.UserCredentials.Any())
            {
                var credentials = new[]
                {
                    new { Username = "mactii", Password = "12345", UserRole = UserRoleEnum.Admin },
                    new { Username = "agatmc", Password = "654321", UserRole = UserRoleEnum.User },
                    new { Username = "alfielev", Password = "132435", UserRole = UserRoleEnum.User },
                    new { Username = "denisegor", Password = "64534231", UserRole = UserRoleEnum.User },
                    new { Username = "camcow", Password = "12345678", UserRole = UserRoleEnum.User }
                };

                for (int i = 0; i < credentials.Length; i++)
                {
                    var credential = credentials[i];
                    _authenticationHelper.CreatePasswordHash(credential.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    var userCredential = new UserCredentials
                    {
                        Username = credential.Username,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        RefreshToken = null,
                        TokenCreated = null,
                        TokenExpires = null,
                        IsActive = true,
                        UserRole = credential.UserRole
                    };

                    userCredentials[i] = userCredential;
                }

                _context.UserCredentials.AddRange(userCredentials);
                _context.SaveChanges();
            }

            return userCredentials;
        }

        private User[] SeedUsers(UserCredentials[] userCredentials)
        {
            User[] users = new User[5];

            if (!_context.Users.Any())
            {
                users[0] = new User
                {
                    Name = "Mateusz",
                    Surname = "Kapka",
                    Email = "mati@gmail.com",
                    PhoneNumber = "123-456-789",
                    Address = "Warszawska 7",
                    DateOfBirth = new DateTime(1999, 7, 24),
                    Gender = GenderEnum.Male,
                    DrivingLicenseNumber = "AUDIT905181XA9UA",
                    IdentificationNumber = "JUV747617",
                    UserCredentials = userCredentials[0]
                };

                users[1] = new User
                {
                    Name = "Agathe",
                    Surname = "McMaster",
                    Email = null,
                    PhoneNumber = "687-453-8992",
                    Address = "6295 Barnett Trail",
                    DateOfBirth = new DateTime(1989, 3, 23),
                    Gender = GenderEnum.Female,
                    DrivingLicenseNumber = "PALIN606125KA9AP",
                    IdentificationNumber = "KYF355839",
                    UserCredentials = userCredentials[1]
                };

                users[2] = new User
                {
                    Name = "Alfie",
                    Surname = "Leving",
                    Email = "alevingm@wordpress.com",
                    PhoneNumber = "908-118-0416",
                    Address = "0 Blaine Way",
                    DateOfBirth = new DateTime(1987, 9, 1),
                    Gender = GenderEnum.Male,
                    DrivingLicenseNumber = "JACKS751077OL9AJ",
                    IdentificationNumber = "TQI843772",
                    UserCredentials = userCredentials[2]
                };

                users[3] = new User
                {
                    Name = "Denise",
                    Surname = "O'Gorman",
                    Email = null,
                    PhoneNumber = "297-176-0374",
                    Address = "5 Arizona Center",
                    DateOfBirth = new DateTime(1999, 2, 18),
                    Gender = GenderEnum.Female,
                    DrivingLicenseNumber = "EMBUR652263JA9ME",
                    IdentificationNumber = "VJW869388",
                    UserCredentials = userCredentials[3]
                };

                users[4] = new User
                {
                    Name = "Camila",
                    Surname = "Cowlard",
                    Email = "ccowlard1b@shutterfly.com",
                    PhoneNumber = "599-492-4167",
                    Address = "3780 Knutson Circle",
                    DateOfBirth = new DateTime(1956, 10, 3),
                    Gender = GenderEnum.Female,
                    DrivingLicenseNumber = "TATE9658255GL9AT",
                    IdentificationNumber = "VMM625071",
                    UserCredentials = userCredentials[4]
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }

            return users;
        }

        private Car[] SeedCars()
        {
            Car[] cars = new Car[10];

            if (!_context.Cars.Any())
            {
                cars[0] = new Car { Make = "Lotus", Model = "Esprit", RegistrationNumber = "1FTEX1C87A" };
                cars[1] = new Car { Make = "Chrysler", Model = "LHS", RegistrationNumber = "WAUSH78E67" };
                cars[2] = new Car { Make = "Ford", Model = "Focus", RegistrationNumber = "WA1VMAFEXA" };
                cars[3] = new Car { Make = "Lexus", Model = "GS", RegistrationNumber = "TRURD38JX8" };
                cars[4] = new Car { Make = "Mitsubishi", Model = "Lancer", RegistrationNumber = "1FTEX1C87A" };
                cars[5] = new Car { Make = "GMC", Model = "Yukon XL 1500", RegistrationNumber = "SCFEBBAC0A" };
                cars[6] = new Car { Make = "Mercedes-Benz", Model = "SL-Class", RegistrationNumber = "WAUYGAFCXC" };
                cars[7] = new Car { Make = "Kia", Model = "Sportage", RegistrationNumber = "1D7RE3GK7B" };
                cars[8] = new Car { Make = "Volkswagen", Model = "Passat", RegistrationNumber = "2G4GR5EK9C" };
                cars[9] = new Car { Make = "Suzuki", Model = "Daewoo Lacetti", RegistrationNumber = "1N6AF0LX8E" };

                _context.Cars.AddRange(cars);
                _context.SaveChanges();
            }

            return cars;
        }

        private void SeedRentals(User[] users, Car[] cars)
        {
            if (!_context.Rentals.Any())
            {
                _context.Rentals.AddRange(
                    new Rental { RentDate = new DateTime(2023, 5, 6), User = users[0], Car = cars[0]},
                    new Rental { RentDate = new DateTime(2023, 3, 23), ReturnDate = new DateTime(2023, 9, 23), User = users[1], Car = cars[1] }
                );
                _context.SaveChanges();
            }
        }
    }
}
