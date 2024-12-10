﻿using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
	public class Media:IMedia
	{
		public string UploadFile(IFormFile file, string folderName)
		{
			string current = Directory.GetCurrentDirectory();
			string fileName = $"{Guid.NewGuid()}{file.FileName}";

			string fullFilePath = Path.Combine(current, "wwwroot", "images", folderName, fileName);
			string subPath = Path.Combine("Images", folderName, fileName);

			var FileStream = new FileStream(fullFilePath, FileMode.Create);
			file.CopyTo(FileStream);
			FileStream.Close();
			return subPath.Replace('\\', '/');
		}
		public void DeleteFile(string folderName, string? fileName)
		{
			if (fileName == null) return;

			string current = Directory.GetCurrentDirectory();
			string filePath = Path.Combine(current, "wwwroot", "images", folderName, fileName);
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			return;
		}
	}
}
