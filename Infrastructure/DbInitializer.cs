using Application.Helpers;
using Domain.Entities;
using Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ComplaintDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ComplaintDBContext>>()))
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                AuthenticationHelper.CreatePasswordHash("123", out passwordHash, out passwordSalt);
                context.Users.Add(new User
                {
                    Id = 1,
                    FirstName = "Rashid",
                    LastName = "Hussain",
                    Username = "rashidhussain",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                });
                context.Users.Add(new User
                {
                    Id = 2,
                    FirstName = "Ghulam",
                    LastName = "Ali",
                    Username = "ali",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                });

                context.Complaint.Add(new Complaint
                {
                    Id = 1,
                    CustomerName = "Rashid Hussain",
                    CustomerContact = "03145360520",
                    CustomerCity = "Rawalpindi",
                    ComplaintType = "Internet Connectivity Issue",
                    ComplaintDescription = "When user connects to the enter he gets limited connection.",
                    DateOfComplaint = DateTime.Now,
                    RegisteredBy = "admin",
                    NameOfTechnician = "Muhammad Ali",
                    Status = "Pending"
                    
                });
                context.SaveChanges();
            }
        }
    }
}
