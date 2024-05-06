using E_Commerce.Core.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
	{
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) 
        { 
        }
        public DbSet<Address> Addresses { get; set; }
    }
}
