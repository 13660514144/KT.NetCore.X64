using KT.Common.Core.Helpers;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace KT.Quanta.WebApi.Controllers.Common
{
    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private AppSettings _appSettings;

        public ClientController(ILogger<ClientController> logger,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        [HttpGet("get/{version?}/application/{path1?}/{path2?}/{path3?}/{path4?}/{path5?}/{path6?}/{path7?}/{path8?}/{path9?}")]
        public IActionResult GetVersionFile(string application,
            string version,
            string path1,
            string path2,
            string path3,
            string path4,
            string path5,
            string path6,
            string path7,
            string path8,
            string path9)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "clients",
                version,
                application,
                path1,
                path2,
                path3,
                path4,
                path5,
                path6,
                path7,
                path8,
                path9);
            var fileStream = System.IO.File.OpenRead(filePath);
            return File(fileStream, "application/octet-stream", Path.GetFileName(filePath));
        }

    }
}
