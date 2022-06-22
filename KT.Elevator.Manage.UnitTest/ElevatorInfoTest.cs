using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KT.Elevator.Manage.UnitTest
{
    [TestClass]
    public class ElevatorInfoTest
    {
        private string _id = IdUtil.NewId();

        [TestMethod]
        public void AddTest()
        {
            var model = new ElevatorInfoModel();
            model.Id = _id;
            model.RealId = "0";
            model.Name = "1ºÅ";
        }
    }
}
