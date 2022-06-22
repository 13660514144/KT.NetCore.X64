using KT.Common.WebApi.HttpApi;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.UnitTest
{
    public class ApiRequestUrl : HttpApiBase
    {
        public override string BaseUrl => "http://127.0.0.1:23260";

        public ApiRequestUrl(ILogger logger) : base(logger)
        {
        }
    }
}
