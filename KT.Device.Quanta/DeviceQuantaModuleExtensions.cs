using KT.Device.Quanta.Communicators;
using KT.Device.Quanta.Devices;
using KT.Device.Quanta.Instances;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Device.Quanta
{
    public static class DeviceQuantaModuleExtensions
    {
        public static void RegisterDeviceQuantaTypes(this IServiceCollection services)
        {
            services.AddSingleton<IQscs3600pCommunicatorInstance, Qscs3600pCommunicatorInstance>();
            services.AddSingleton<ITurnstileProcessorQscs3600pDeviceInstance, TurnstileProcessorQscs3600pDeviceInstance>();
            services.AddTransient<ITurnstileProcessorQscs3600pDevice, TurnstileProcessorQscs3600pDevice>();
            services.AddTransient<IQscs3600pCommunicator, Qscs3600pCommunicator>();
        }

        public static async Task InitializeDeviceQuantaAsync(IServiceProvider serviceProvider)
        {
            //初始化设备监控与工厂
            var turnstileProcessorQscs3600pDeviceInstance = serviceProvider.GetRequiredService<ITurnstileProcessorQscs3600pDeviceInstance>();
            await turnstileProcessorQscs3600pDeviceInstance.StartAsync();

            var qscs3600pCommunicatorInstance = serviceProvider.GetRequiredService<IQscs3600pCommunicatorInstance>();
            await qscs3600pCommunicatorInstance.StartAsync();


            //初始化数据，需初始化完成设备监控与工厂才能初始化数据
            await turnstileProcessorQscs3600pDeviceInstance.InitializeAsync();
        }
    }
}
