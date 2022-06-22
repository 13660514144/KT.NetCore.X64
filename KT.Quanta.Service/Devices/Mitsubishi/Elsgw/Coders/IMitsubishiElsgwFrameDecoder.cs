using DotNetty.Transport.Channels.Sockets;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders
{
    public interface IMitsubishiElsgwFrameDecoder
    {
        MitsubishiElsgwTransmissionHeader Decode(DatagramPacket input);
    }
}