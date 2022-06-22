using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDistribute
{
    public interface IPassRightDistribute
    {
        Task AddOrUpdate(PassRightModel model);
        Task DeleteAsync(string id, long time);
    }
}
