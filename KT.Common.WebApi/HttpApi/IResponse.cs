using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WebApi.HttpApi
{
    public interface IResponse
    {
        int Code { get; set; }
        string Message { get; set; } 
    }
}
