﻿using ECommerce_be.Application.Abstractions.Storage.Local;
using ECommerce_be.Infrastructure.StaticServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ECommerce_be.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName) => File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(file => file.Name).ToList();
        }

        public bool HasFile(string path, string fileName) => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string pathOrContainerName)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileName = NameManager.GenerateFileName(uploadPath, file.FileName);
                bool result = await SaveFileAsync($"{uploadPath}\\{fileName}", file);
                datas.Add((fileName, path));
                results.Add(result);
            }

            if (results.TrueForAll(x => x.Equals(true)))
                return datas;

            // TODO: File upload exception has occurred

            return null;
        }

        private async Task<bool> SaveFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                // TODO: It should be fixed when the log structure is created.
                throw;
            }
        }

    }
}
