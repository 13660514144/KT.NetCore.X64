using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDistribute
{
    public interface ICardDeviceDistribute
    {
        Task AddOrUpdateAsync(CardDeviceModel model);
        Task DeleteAsync(string processorKey, string id, long time);
    }
}
