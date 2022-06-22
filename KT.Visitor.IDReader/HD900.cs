using IDevices;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace KT.Visitor.IdReader
{
    public class HD900 : IDevice
    {
        public ReaderTypeEnum ReaderType { get; } = ReaderTypeEnum.HD900;
        public Action<object> ResultCallBack { get; set; }

        public HD900()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public int Port { get; set; } 
        private int _minPort = 1001;
        private int _maxPort = 1016;

        public bool InitComm()
        {
            int iPort, iRetUSB = 0;
            for (iPort = _minPort; iPort <= _maxPort; iPort++)
            {
                iRetUSB = HD900SDK.HD_InitComm(iPort);
                if (iRetUSB == 0)
                {
                    Port = iPort;
                    break;
                }
            }
            if (iRetUSB != 0)//0是成功
            {
                throw IdReaderException.Run($"设备连接失败,iRetUSB:{iRetUSB}");
                
            }
            return true;
        }

        public void StartSignalLamp()
        {
        }

        public bool Authenticate()
        {
            int r = HD900SDK.HD_Authenticate(0);
            if (r != 0)
            {
                throw IdReaderException.Run("卡片认证失败");
            }
            return true;
        }

        public Person ReadContent()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\zp.bmp";
            string message= System.IO.Directory.GetCurrentDirectory() + "\\cardmsg.txt";
            Person person = new Person();
            //阅读器分离
            /*if (!File.Exists(message))
            {
                return person;
            }
            StringBuilder sp = new StringBuilder();
            try
            {
                StreamReader sr = new StreamReader(message, System.Text.Encoding.GetEncoding("gb2312"));
                string line;                
                while ((line = sr.ReadLine()) != null)
                {
                    sp.Append(line);
                }
                sr.Close();
                sr.Dispose();            
                File.Delete(message);
            }
            catch (Exception ex)
            {  }
            JObject OO = JObject.Parse(sp.ToString());
            person.Name = OO["pName"].ToString().Trim();
            person.Gender = OO["pSex"].ToString().Trim();
            person.Nation = OO["pNation"].ToString().Trim();
            person.Birthday = OO["pBirth"].ToString().Trim();
            person.Address = OO["pAddress"].ToString().Trim();
            person.IdCode = OO["pCertNo"].ToString().Trim();
            person.Agency = OO["pDepartment"].ToString().Trim();
            person.ExpireStart = OO["pEffectData"].ToString().Trim();
            person.ExpireEnd = OO["pExpire"].ToString().Trim();
            person.CardType = OO["CardType"].ToString().Trim();

            string str = JsonConvert.SerializeObject(person);
            string ff = @"code.txt";
            if (File.Exists(ff))
            {
                File.Delete(ff);
            }
            using (StreamWriter sw = File.CreateText(ff))
            {
                sw.WriteLine(str);
                sw.Close();
                sw.Dispose();
            }
            */
            //2022-03-22
            byte[] pname = new byte[100], 
                psex = new byte[50], 
                pnation = new byte[100], 
                pbirth = new byte[256], 
                paddress = new byte[256], 
                pcert = new byte[256], 
                pdepart = new byte[256], peff = new byte[256], pexp = new byte[256];            
            
            var t = HD900SDK.HD_ReadCard();
            if (t != 0)
            {
                throw IdReaderException.Run($"读取身份证件失败！t={t}");
            }
            int Itype = HD900SDK.GetCardType();
            int s = HD900SDK.GetName(pname);
             s = HD900SDK.GetSex(psex);
             s = HD900SDK.GetNation(pnation);
             s = HD900SDK.GetBirth(pbirth);
             s = HD900SDK.GetAddress(paddress);
             s = HD900SDK.GetCertNo(pcert);
             s = HD900SDK.GetEffectDate(peff);
             s = HD900SDK.GetExpireDate(pexp);
          
            person.Name = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(pname);
            person.Gender = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(psex);
            switch (Itype)
            {
                case 0:
                    person.CardType = "ID_CARD";
                    break;
                case 1:
                    person.CardType = "OTHER";
                    break;
                case 2:
                    person.CardType = "OTHER";
                    break;
            }
  
            person.Nation = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(pnation);
            person.Birthday = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(pbirth);
            person.Address = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(paddress);
            person.IdCode = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(pcert); 
            person.Agency = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(pdepart);
            person.ExpireStart = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(peff);
            person.ExpireEnd = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetString(pexp);
            person.Gender = person.Gender.Substring(0,1);
            s = HD900SDK.GetBmpFile(path);
            string str = JsonConvert.SerializeObject(person);
            string ff = @"code.txt";
            if (File.Exists(ff))
            {
                File.Delete(ff);
            }
            using (StreamWriter sw = File.CreateText(ff))
            {
                sw.WriteLine(str);
                sw.Close();
                sw.Dispose();
            }
            
            //加载图片
            Image image = Bitmap.FromFile(path);
            if (image == null)
            {
                throw IdReaderException.Run("图片未加载成功");
            }
            Image bmp = new Bitmap(image);
            image.Dispose();
            person.Portrait = bmp;

            //保存头像到文件夹
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files\\Images\\Portraits");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            imagePath = Path.Combine(imagePath, person.IdCode + ".bmp");
            ImageHelper.SaveToFile(person.Portrait, imagePath, true, ImageFormat.Bmp);

            return person;
        }

        public bool CloseComm()
        {
            int r = HD900SDK.HD_CloseComm(1001);
            if (r != 0)
            {
                throw IdReaderException.Run("关闭设备失败");
            }
            return true;
        }
         
        public Person ScanContent(string operateIdType)
        {
            throw new NotImplementedException();
        }
    }
}
