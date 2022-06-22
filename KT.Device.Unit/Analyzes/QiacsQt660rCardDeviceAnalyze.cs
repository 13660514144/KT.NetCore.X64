using KT.Device.Unit.Analyzes;
using Microsoft.Extensions.Logging;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class QiacsQt660rCardDeviceAnalyze : DefaultQrCodeAnalyze, IQiacsQt660rCardDeviceAnalyze
    {
        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;

        public QiacsQt660rCardDeviceAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider)
            : base(logger, qrCodeAnalyzeProvider)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
        }
    }
}
