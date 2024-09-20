﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Supershop.Helpers
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);

        Task<Guid> UploadBlobAsync(byte[] file, string containerNamed);

        Task<Guid> UploadBlobAsync(string imagee, string containerName);
    }
}