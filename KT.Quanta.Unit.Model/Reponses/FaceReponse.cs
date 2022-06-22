using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Reponses
{
    public class FaceReponse : QuantaSerializer
    {
        public byte[] FaceBytes { get; set; }

        public int FaceSize { get; set; }

        public string FaceUrl { get; set; }

        protected override void Read()
        {
            FaceBytes = ReadBytes();
            FaceSize = ReadInt();
            FaceUrl = ReadString();
        }

        protected override void Write()
        {
            WriteBytes(FaceBytes);
            WriteInt(FaceSize);
            WriteString(FaceUrl);
        }
    }
}
