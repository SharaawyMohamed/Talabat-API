using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
	public interface IMedia
	{
		string UploadFile(IFormFile file, string folderName);
		void DeleteFile(string folderName, string? fileName);
	}
}
