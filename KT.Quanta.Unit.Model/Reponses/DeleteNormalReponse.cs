using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Reponses
{
    public class DeleteNormalReponse : QuantaSerializer
    {
        public string Id { get; set; }

        public long EditTime { get; set; }

        protected override void Read()
        {
            Id = ReadString();
            EditTime = ReadLong();
        }

        protected override void Write()
        {
            WriteString(Id);
            WriteLong(EditTime);
        }
    }
}
