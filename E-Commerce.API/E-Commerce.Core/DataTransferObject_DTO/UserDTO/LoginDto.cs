﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.UserServices
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Email Is Required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required")]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", ErrorMessage = "Password must be contains [a-z],[A-Z]/[0-9] and at least on symbol")]
		public string Password { get; set; }

	}
}
