using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDistribute
{
    public interface IRightGroupDistribute
    {
        Task AddOrUpdate(CardDeviceRightGroupModel model);
        Task Delete(string id, long time);
    }
}
