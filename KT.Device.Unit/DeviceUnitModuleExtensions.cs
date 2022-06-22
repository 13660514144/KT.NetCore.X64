using KT.Device.Unit.CardReaders.Datas;
using KT.Device.Unit.Devices;
using KT.Device.Unit.Instances;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Threading.Tasks;

namespace KT.Device.Unit
{
    public static class DeviceUnitModuleExtensions
    {
        public static void RegisterDeviceUnitTypes(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INlsFm25rCardDeviceAnalyze, NlsFm25rCardDeviceAnalyze>();
            containerRegistry.Register<IQiacsQt660rCardDeviceAnalyze, QiacsQt660rCardDeviceAnalyze>();
            containerRegistry.Register<IQiacsR824CardDeviceAnalyze, QiacsR824CardDeviceAnalyze>();
            containerRegistry.Register<IQiacsR992CardDeviceAnalyze, QiacsR992CardDeviceAnalyze>();
            containerRegistry.Register<IQscsBx5nCardDeviceAnalyze, QscsBx5nCardDeviceAnalyze>();
            containerRegistry.Register<IQscsBx5xCardDeviceAnalyze, QscsBx5xCardDeviceAnalyze>();
            containerRegistry.Register<IQscsBxenCardDeviceAnalyze, QscsBxenCardDeviceAnalyze>();
            containerRegistry.Register<IQscsR811CardDeviceAnalyze, QscsR811CardDeviceAnalyze>();

            containerRegistry.Register<INlsFm25rCardDevice, NlsFm25rCardDevice>();
            containerRegistry.Register<IQiacsQt660rCardDevice, QiacsQt660rCardDevice>();
            containerRegistry.Register<IQiacsR824CardDevice, QiacsR824CardDevice>();
            containerRegistry.Register<IQiacsR992CardDevice, QiacsR992CardDevice>();
            containerRegistry.Register<IQscsBx5nCardDevice, QscsBx5nCardDevice>();
            containerRegistry.Register<IQscsBx5xCardDevice, QscsBx5xCardDevice>();
            containerRegistry.Register<IQscsBxenCardDevice, QscsBxenCardDevice>();
            containerRegistry.Register<IQscsR811CardDevice, QscsR811CardDevice>();

            containerRegistry.RegisterSingleton<INlsFm25rCardDeviceInstance, NlsFm25rCardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQiacsQt660rCardDeviceInstance, QiacsQt660rCardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQiacsR824CardDeviceInstance, QiacsR824CardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQiacsR992CardDeviceInstance, QiacsR992CardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQscsBx5nCardDeviceInstance, QscsBx5nCardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQscsBx5xCardDeviceInstance, QscsBx5xCardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQscsBxenCardDeviceInstance, QscsBxenCardDeviceInstance>();
            containerRegistry.RegisterSingleton<IQscsR811CardDeviceInstance, QscsR811CardDeviceInstance>();
        }

        public static async Task InitializeDeviceUnitAsync(this IContainerProvider containerProvider)
        {
            var nlsFm25rCardDeviceInstance = containerProvider.Resolve<INlsFm25rCardDeviceInstance>();
            await nlsFm25rCardDeviceInstance.StartAsync();
            var qiacsQt660rCardDeviceInstance = containerProvider.Resolve<IQiacsQt660rCardDeviceInstance>();
            await qiacsQt660rCardDeviceInstance.StartAsync();
            var qiacsR824CardDeviceInstance = containerProvider.Resolve<IQiacsR824CardDeviceInstance>();
            await qiacsR824CardDeviceInstance.StartAsync();
            var qiacsR992CardDeviceInstance = containerProvider.Resolve<IQiacsR992CardDeviceInstance>();
            await qiacsR992CardDeviceInstance.StartAsync();
            var qscsBx5nCardDeviceInstance = containerProvider.Resolve<IQscsBx5nCardDeviceInstance>();
            await qscsBx5nCardDeviceInstance.StartAsync();
            var qscsBx5xCardDeviceInstance = containerProvider.Resolve<IQscsBx5xCardDeviceInstance>();
            await qscsBx5xCardDeviceInstance.StartAsync();
            var qscsBxenCardDeviceInstance = containerProvider.Resolve<IQscsBxenCardDeviceInstance>();
            await qscsBxenCardDeviceInstance.StartAsync();
            var qscsR811CardDeviceInstance = containerProvider.Resolve<IQscsR811CardDeviceInstance>();
            await qscsR811CardDeviceInstance.StartAsync();
        }
    }
}
