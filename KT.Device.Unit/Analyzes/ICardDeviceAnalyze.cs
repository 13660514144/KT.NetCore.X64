using KT.Device.Unit.CardReaders.Models;

namespace KT.Device.Unit.CardReaders.Datas
{
    public interface ICardDeviceAnalyze
    {
        CardReceiveModel Analyze(string protName, byte[] datas);
    }
}
