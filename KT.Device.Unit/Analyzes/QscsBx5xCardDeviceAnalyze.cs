using KT.Device.Unit.Analyzes;
using Microsoft.Extensions.Logging;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class QscsBx5xCardDeviceAnalyze : DefaultQrCodeAnalyze, IQscsBx5xCardDeviceAnalyze
    {
        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;

        public QscsBx5xCardDeviceAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider)
            : base(logger, qrCodeAnalyzeProvider)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
        }
    }
}
