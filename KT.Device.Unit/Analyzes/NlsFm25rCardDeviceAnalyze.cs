using KT.Device.Unit.Analyzes;
using Microsoft.Extensions.Logging;

namespace KT.Device.Unit.CardReaders.Datas
{
    public class NlsFm25rCardDeviceAnalyze : DefaultQrCodeAnalyze, INlsFm25rCardDeviceAnalyze
    {
        private readonly ILogger _logger;
        private readonly QrCodeAnalyzeProvider _qrCodeAnalyzeProvider;

        public NlsFm25rCardDeviceAnalyze(ILogger logger,
            QrCodeAnalyzeProvider qrCodeAnalyzeProvider)
            : base(logger, qrCodeAnalyzeProvider)
        {
            _logger = logger;
            _qrCodeAnalyzeProvider = qrCodeAnalyzeProvider;
        }
    }
}
