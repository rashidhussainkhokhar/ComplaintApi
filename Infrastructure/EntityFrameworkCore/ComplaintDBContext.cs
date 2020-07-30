using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore
{
    public class ComplaintDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public ComplaintDBContext(DbContextOptions<ComplaintDBContext> options): base(options)
        {
            
        }
    }
}
