using System.Threading;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Helpers
{
    public class CacheKeys
    {
        public static string VisitorConfigParms => "_VisitorConfigParms";
        public static string VisitorSetting => "_VisitorSetting";
        public static string VisiotrImportCompanyStaff => "_VisiotrImportCompanyStaff";
        public static string ElevatorFloors => "_ElevatorFloors";


        public static CancellationTokenSource ElevatorFloorsCancellationTokenSource = new CancellationTokenSource();


        /// <summary>
        /// 重置缓存
        /// </summary>
        /// <param name="cancellationTokenSource"></param>
        public static Task<CancellationTokenSource> ResetAsync(CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested && cancellationTokenSource.Token.CanBeCanceled)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }

            cancellationTokenSource = new CancellationTokenSource();

            return Task.FromResult(cancellationTokenSource);
        }
    }
}
