using IDevices;
using KT.Proxy.BackendApi.Enums;
using KT.Visitor.IdReader.Common;
using System;
using System.Drawing;
using System.IO;

namespace KT.Visitor.IdReader
{
    public class Development : IDevice
    {
        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.DEVELOPMENT;
        public Action<object> ResultCallBack { get; set; }

        public bool Authenticate()
        {
            return true;
        }

        public bool CloseComm()
        {
            return true;
        }

        public bool InitComm()
        {
            return true;
        }

        public void StartSignalLamp()
        {
        }

        public Person ReadContent()
        {
            Person person = new Person();
            person.Agency = "天水市秦州区公安分局";
            person.Address = "甘肃省天水市秦州区";
            person.Birthday = "1999-06-01";
            person.IdCode = "445221199407186960";
            person.CardType = CertificateTypeEnum.ID_CARD.Value;
            person.Name = "吴丽贞";
            person.Nation = "中华人民共和国";
            person.Gender = "女";
            person.ExpireStart = "1999-01-01";
            person.ExpireEnd = "2020-03-01";

            var faceUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReferenceFiles/Files/Images/face-demo.bmp");
            if (File.Exists(faceUrl))
            {
                person.Portrait = Image.FromFile(faceUrl);
            }

            return person;
        }

        public Person ScanContent(string operateIdType)
        {
            throw new System.NotImplementedException();
        }
    }
}
