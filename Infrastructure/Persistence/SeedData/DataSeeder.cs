using Application.Interfaces.Helpers;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
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

        private readonly ApplicationDbContext _context;
        private readonly IAuthenticationHelper _authenticationHelper;

        public DataSeeder(ApplicationDbContext context, IAuthenticationHelper authenticationHelper)
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

            var blogs = SeedBlogs();
            SeedComments(blogs);
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
                    Email = "agathemcmaster@gmail.com",
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
                    Email = "deniseogorman@gmail.com",
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
                cars[0] = new Car
                {
                    Make = "Lotus",
                    Model = "Esprit",
                    RegistrationNumber = "1FTEX1C87A",
                    Year = 1999,
                    Color = ColorEnum.Black,
                    PricePerDay = 400.5m,
                    IsAvailable = true,
                    Engine = "Automatic",
                    Speed = 220,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 20,
                    SeatType = "Heated seats",
                    Description = "The Lotus Esprit is an iconic sports car known for its distinctive wedge-shaped " +
                    "design and impressive performance. Manufactured by Lotus, it gained fame through appearances in media " +
                    "like James Bond films. With agile handling and various engine options, the Esprit is a symbol of automotive innovation."
                };
                cars[1] = new Car
                {
                    Make = "Chrysler",
                    Model = "LHS",
                    RegistrationNumber = "WAUSH78E67",
                    Year = 2001,
                    Color = ColorEnum.Black,
                    PricePerDay = 350m,
                    IsAvailable = true,
                    Engine = "Automatic",
                    Speed = 200,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 43,
                    SeatType = "Heated seats",
                    Description = "The Chrysler LHS is a luxurious sedan renowned for its elegant design, comfort, " +
                    "and refined driving experience. It embodies a harmonious blend of style and sophistication, offering spaciousness, " +
                    "premium materials, and modern technology. With its powerful engine and smooth suspension, " +
                    "the LHS delivers both performance and a comfortable ride."
                };
                cars[2] = new Car
                {
                    Make = "Ford",
                    Model = "Focus",
                    RegistrationNumber = "WA1VMAFEXA",
                    Year = 2002,
                    Color = ColorEnum.White,
                    PricePerDay = 300.5m,
                    IsAvailable = true,
                    Engine = "Manual",
                    Speed = 200,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 54,
                    SeatType = "Heated seats",
                    Description = "The Ford Focus is a compact car known for its practicality, efficiency, " +
                    "and enjoyable driving experience. With a range of body styles, it offers a comfortable interior, " +
                    "modern technology, and agile handling, making it a versatile choice for urban and daily driving."
                };
                cars[3] = new Car
                {
                    Make = "Lexus",
                    Model = "GS",
                    RegistrationNumber = "TRURD38JX8",
                    Year = 2010,
                    Color = ColorEnum.Black,
                    PricePerDay = 650m,
                    IsAvailable = true,
                    Engine = "Manual",
                    Speed = 180,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 15,
                    SeatType = "Heated seats",
                    Description = "The Lexus GS is a luxury sedan that seamlessly blends sophisticated design, " +
                    "advanced technology, and refined performance. With its opulent interior, cutting-edge features, " +
                    "and dynamic driving capabilities, it embodies Lexus's commitment to providing an elevated driving experience."
                };
                cars[4] = new Car
                {
                    Make = "Mitsubishi",
                    Model = "Lancer",
                    RegistrationNumber = "1FTEX1C87A",
                    Year = 2005,
                    Color = ColorEnum.Red,
                    PricePerDay = 570m,
                    IsAvailable = true,
                    Engine = "Automatic",
                    Speed = 190,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 34,
                    SeatType = "Heated seats",
                    Description = "\r\nThe Mitsubishi Lancer is a reliable compact car known for its practicality, affordability, " +
                    "and consistent performance. With its compact size, comfortable interior, and efficient design, " +
                    "it offers a budget-friendly option for everyday driving needs."
                };
                cars[5] = new Car
                {
                    Make = "GMC",
                    Model = "Yukon XL 1500",
                    RegistrationNumber = "SCFEBBAC0A",
                    Year = 2007,
                    Color = ColorEnum.Purple,
                    PricePerDay = 340,
                    IsAvailable = true,
                    Engine = "Automatic",
                    Speed = 220,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 29,
                    SeatType = "Heated seats",
                    Description = "The GMC Yukon XL 1500 is a spacious full-size SUV known for its powerful capabilities and modern features. " +
                    "With its extended size, advanced technology, and robust engine options, it's an ideal choice for those seeking " +
                    "a versatile and capable vehicle."
                };
                cars[6] = new Car
                {
                    Make = "Mercedes-Benz",
                    Model = "SL-Class",
                    RegistrationNumber = "WAUYGAFCXC",
                    Year = 2010,
                    Color = ColorEnum.Black,
                    PricePerDay = 250,
                    IsAvailable = true,
                    Engine = "Manual",
                    Speed = 200,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 37,
                    SeatType = "Heated seats",
                    Description = "The Mercedes-Benz SL-Class is a luxury convertible that offers timeless elegance, " +
                    "high-performance capabilities, and advanced features. With its iconic design, opulent interior, " +
                    "and powerful engines, it's the epitome of luxurious driving."
                };
                cars[7] = new Car
                {
                    Make = "Kia",
                    Model = "Sportage",
                    RegistrationNumber = "1D7RE3GK7B",
                    Year = 1990,
                    Color = ColorEnum.Black,
                    PricePerDay = 80m,
                    IsAvailable = true,
                    Engine = "Automatic",
                    Speed = 190,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 65,
                    SeatType = "Heated seats",
                    Description = "The Kia Sportage is a compact SUV known for its versatility, modern features, and comfortable ride. " +
                    "With its spacious interior, user-friendly technology, and efficient engines, it offers a practical choice " +
                    "for various driving needs."
                };
                cars[8] = new Car
                {
                    Make = "Volkswagen",
                    Model = "Passat",
                    RegistrationNumber = "2G4GR5EK9C",
                    Year = 2005,
                    Color = ColorEnum.Green,
                    PricePerDay = 320.6m,
                    IsAvailable = true,
                    Engine = "Manual",
                    Speed = 200,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 38,
                    SeatType = "Heated seats",
                    Description = "The Volkswagen Passat is a midsize sedan known for its refined design, practical features, " +
                    "and balanced performance. With its spacious interior, modern technology, and comfortable ride, it offers " +
                    "a well-rounded choice for drivers seeking a combination of style and functionality."
                };
                cars[9] = new Car
                {
                    Make = "Suzuki",
                    Model = "Daewoo Lacetti",
                    RegistrationNumber = "1N6AF0LX8E",
                    Year = 2012,
                    Color = ColorEnum.Gray,
                    PricePerDay = 550.3m,
                    IsAvailable = true,
                    Engine = "Automatic",
                    Speed = 150,
                    Image = Array.Empty<byte>(),
                    GPS = "GPS Navigation",
                    Ratings = 13,
                    SeatType = "Heated seats",
                    Description = "\r\nThe Suzuki Daewoo Lacetti (Chevrolet Optra/Nubira) is a compact car known for its affordability, " +
                    "practicality, and simple driving experience. With its compact size and functional design, it offers essential " +
                    "space and cost-effective transportation for daily needs."
                };

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
                    new Rental { RentDate = new DateTime(2023, 5, 6), User = users[0], Car = cars[0] },
                    new Rental { RentDate = new DateTime(2023, 3, 23), ReturnDate = new DateTime(2023, 9, 23), User = users[1], Car = cars[1] }
                );
                _context.SaveChanges();
            }
        }

        private Blog[] SeedBlogs()
        {
            Blog[] blogs = new Blog[3];
            if (!_context.Blogs.Any())
            {
                blogs[0] = new Blog
                {
                    Title = "The Benefits of Regular Car Maintenance",
                    AuthorName = "John",
                    AuthorSurname = "Smith",
                    Description = "Proper car maintenance is essential for ensuring the longevity and performance of your vehicle. " +
                    "In this blog post, we'll discuss the importance of regular maintenance and provide helpful tips for keeping your car in top shape.",
                    DetailedDescription = "Proper car maintenance is essential for ensuring the longevity and performance of your vehicle. " +
                    "In this blog post, we'll discuss the importance of regular maintenance and provide helpful tips for keeping your " +
                    "car in top shape.\r\n\r\nRegular car maintenance keeps your vehicle healthy, happy, and road-ready." +
                    "\r\nProper car maintenance is essential for ensuring the longevity and performance of your vehicle. " +
                    "In this blog post, we'll discuss the importance of regular maintenance and provide helpful tips for " +
                    "keeping your car in top shape.",
                    Date = new DateTime(2023, 4, 15, 9, 0, 0),
                    Image = Array.Empty<byte>(),
                    Comments = new List<Comment>(1)
                    {
                        new Comment
                        {
                            Name = "Anna",
                            Surname = "Kowalska",
                            Date = new DateTime(2022, 7, 14, 11, 31, 00),
                            Email = "anna.kowalska@gmail.com",
                            Description = "Thank you for sharing these helpful tips on car maintenance. " +
                            "I'm really glad to learn more about taking care of my vehicle. " +
                            "I will definitely use this information to keep my car in the best condition. Thanks again!",
                        }
                    }
                };

                blogs[1] = new Blog
                {
                    Title = "Tips for Jumpstarting a Dead Car Battery",
                    AuthorName = "Emily",
                    AuthorSurname = "Smith",
                    Description = "Dealing with a dead car battery can be frustrating, but knowing how to jumpstart your vehicle can save you from being " +
                    "stranded. This blog post offers step-by-step instructions and helpful tips for safely jumpstarting a dead car battery.",
                    DetailedDescription = "Dealing with a dead car battery can be frustrating, but knowing how to jumpstart your vehicle can save " +
                    "you from being stranded. This blog post offers step-by-step instructions and helpful tips for safely jumpstarting " +
                    "a dead car battery.\r\n\r\nGet back on the road quickly with these tips for jumpstarting a dead car battery." +
                    "\r\nDealing with a dead car battery can be frustrating, but knowing how to jumpstart your vehicle can save " +
                    "you from being stranded. This blog post offers step-by-step instructions and helpful tips for safely " +
                    "jumpstarting a dead car battery.",
                    Date = new DateTime(2023, 6, 21, 9, 0, 0),
                    Image = Array.Empty<byte>(),
                    Comments = new List<Comment>(1)
                    {
                        new Comment
                        {
                            Name = "Anna",
                            Surname = "Kowalska",
                            Date = new DateTime(2022, 7, 15, 11, 31, 00),
                            Email = "anna.kowalska@gmail.com",
                            Description = "Thank you for sharing these helpful tips on car maintenance. " +
                            "I'm really glad to learn more about taking care of my vehicle. I will definitely " +
                            "use this information to keep my car in the best condition. Thanks again!"
                        }
                    }
                };

                blogs[2] = new Blog
                {
                    Title = "Road Trip Essentials: Packing for a Long Drive",
                    AuthorName = "David",
                    AuthorSurname = "Thompson",
                    Description = "Planning a road trip? Make sure you have all the essentials for a comfortable and enjoyable journey. " +
                    "This blog post provides a comprehensive checklist of items to pack, including safety equipment, entertainment options, and more.",
                    DetailedDescription = "Planning a road trip? Make sure you have all the essentials for a comfortable and enjoyable journey. " +
                    "This blog post provides a comprehensive checklist of items to pack, including safety equipment, entertainment options, " +
                    "and more.\r\n\r\nPack your sense of adventure and make every road trip an opportunity for new experiences." +
                    "\r\nPlanning a road trip? Make sure you have all the essentials for a comfortable and enjoyable journey. " +
                    "This blog post provides a comprehensive checklist of items to pack, including safety equipment, entertainment options, and more.",
                    Date = new DateTime(2023, 6, 28, 9, 0, 0),
                    Image = Array.Empty<byte>(),
                    Comments = new List<Comment>(1)
                    {
                        new Comment
                        {
                            Name = "Anna",
                            Surname = "Kowalska",
                            Date = new DateTime(2022, 7, 16, 11, 31, 00),
                            Email = "anna.kowalska@gmail.com",
                            Description = "Thank you for sharing these helpful tips on car maintenance. " +
                            "I'm really glad to learn more about taking care of my vehicle. I will definitely " +
                            "use this information to keep my car in the best condition. Thanks again!"
                        }
                    }
                };

                _context.Blogs.AddRange(blogs);
                _context.SaveChanges();
            }

            return blogs;
        }

        private void SeedComments(Blog[] blogs)
        {
            Comment[] comments = new Comment[3];
            if (!_context.Comments.Any())
            {
                comments[0] = new Comment
                {
                    BlogID = blogs[0].ID,
                    Name = "Anna",
                    Surname = "Kowalska",
                    Date = new DateTime(2022, 7, 14, 11, 31, 00),
                    Email = "anna.kowalska@gmail.com",
                    Description = "Thank you for sharing these helpful tips on car maintenance. " +
                            "I'm really glad to learn more about taking care of my vehicle. " +
                            "I will definitely use this information to keep my car in the best condition. Thanks again!"
                };

                comments[1] = new Comment
                {
                    BlogID = blogs[1].ID,
                    Name = "Anna",
                    Surname = "Kowalska",
                    Date = new DateTime(2022, 7, 15, 11, 31, 00),
                    Email = "anna.kowalska@gmail.com",
                    Description = "Thank you for sharing these helpful tips on car maintenance. " +
                            "I'm really glad to learn more about taking care of my vehicle. I will definitely " +
                            "use this information to keep my car in the best condition. Thanks again!"
                };

                comments[2] = new Comment
                {
                    BlogID = blogs[2].ID,
                    Name = "Anna",
                    Surname = "Kowalska",
                    Date = new DateTime(2022, 7, 16, 11, 31, 00),
                    Email = "anna.kowalska@gmail.com",
                    Description = "Thank you for sharing these helpful tips on car maintenance. " +
                            "I'm really glad to learn more about taking care of my vehicle. I will definitely " +
                            "use this information to keep my car in the best condition. Thanks again!"
                };

                _context.Comments.AddRange(comments);
                _context.SaveChanges();
            }
        }
    }
}
