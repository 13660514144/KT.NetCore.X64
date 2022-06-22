using DotNetty.Buffers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders
{
    public interface IMitsubishiElsgwFrameEncoder
    {
        MitsubishiElsgwTransmissionHeader AssistantEncode(IByteBuffer message);
        IByteBuffer HeaderEncode(MitsubishiElsgwTransmissionHeader message);
    }
}