using KT.Device.Unit.Analyzes;
using Microsoft.Extensions.Logging;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class QscsBxenCardDeviceAnalyze : DefaultQrCodeAnalyze, IQscsBxenCardDeviceAnalyze
    {
        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;

        public QscsBxenCardDeviceAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider)
            : base(logger, qrCodeAnalyzeProvider)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
        }
    }
}
