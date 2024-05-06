using E_Commerce.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
    public class IdentityDataSeed
	{
		public static async Task SeedUserAsync(UserManager<ApplicationUser> usermanager)
		{
			if (usermanager.Users.Count() == 0)
			{
				var user = new ApplicationUser()
				{
					UserName = "Sharawy",
					Email = "sharawym725@gmail.com",
					DisplayName = "Sharawy Mohamed",
					PhoneNumber="01275035357",
					Address = new Address()
					{
						Street = "El-Monshawy Street",
						Country="Egypt",
						City="Sohag",
						State="Al-7amad",
						PostalCode="#120",
					}

				};

				await usermanager.CreateAsync(user, "Pa$$w0rd");
			}
		}
	}
}
