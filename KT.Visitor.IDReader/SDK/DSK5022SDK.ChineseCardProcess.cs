using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using KT.Visitor.IdReader.Common;
using KT.Common.WpfApp.Utils;
using System.Drawing.Imaging;
using KT.Common.Core.Utils;

namespace KT.Visitor.IdReader.SDK
{
    /// <summary>
    /// 专门用来处理中国大陆身份证信息的读取与显示
    /// </summary>
    public partial class DSK5022SDK
    {
        public USB_SDK_CERTIFICATE_INFO CertificateInfo;

        public DSK5022SDK(ref USB_SDK_CERTIFICATE_INFO PCertificateInfo)
        {
            CertificateInfo = PCertificateInfo;
        }
        public DSK5022SDK()
        {

        }
        public Person ReadCertificateInfo()
        {
            return ProcessChineseCardInfo();
        }

        private Person ProcessChineseCardInfo()
        {
            var person = new Person();

            person.Name = ReadChineseIDcardName();
            person.Gender = ReadChineseCardSex();
            person.Birthday = ReadBirthDate();
            person.Address = ReadHomeAddress();
            person.Nation = ReadChineseNationality();
            person.IdCode = ReadIDNumber();
            person.Portrait = ReadPortrait();

            return person;
        }

        private string ReadChineseIDcardName()
        {
            byte[] ChineseIDName = new byte[30];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 0, ChineseIDName, 0, 30);
            string SName = Encoding.Unicode.GetString(ChineseIDName);

            return SName;
        }

        private string ReadChineseCardSex()
        {
            byte[] ChineseSex = new byte[2];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 30, ChineseSex, 0, 2);
            string ChineseCardSex = Encoding.Unicode.GetString(ChineseSex);
            if (ChineseCardSex == "1")
            {
                ChineseCardSex = "男";
            }
            else
            {
                ChineseCardSex = "女";
            }

            return ChineseCardSex;
        }

        private string ReadChineseNationality()
        {
            byte[] ByteNationality = new byte[4];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 32, ByteNationality, 0, 4);

            string nationality = Encoding.Unicode.GetString(ByteNationality);
            nationality = DSK5022SDK.GetNationality(ref nationality);

            return nationality;
        }

        private string ReadBirthDate()
        {
            byte[] Bbirthdate = new byte[16];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 36, Bbirthdate, 0, 16);
            string BirthDate = Encoding.Unicode.GetString(Bbirthdate);
            StandardFormalOfBrithDate(ref BirthDate);

            return BirthDate;
        }

        private void StandardFormalOfBrithDate(ref string BirthDate)
        {
            string yyyy = BirthDate.Substring(0, 4);
            string mm = BirthDate.Substring(4, 2);
            string dd = BirthDate.Substring(6, 2);
            BirthDate = yyyy + "-" + mm + "-" + dd;
        }

        private string ReadHomeAddress()
        {
            byte[] HomeAddress = new byte[70];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 52, HomeAddress, 0, 70);

            string Address = Encoding.Unicode.GetString(HomeAddress);

            return Address;
        }

        private string ReadIDNumber()
        {
            byte[] IDnumber = new byte[36];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 122, IDnumber, 0, 36);
            string CertificateNum = Encoding.Unicode.GetString(IDnumber);

            return CertificateNum;
        }

        private string photoName;
        private Image ReadPortrait()
        {
            return IDpictureProcess();
        }

        private Image IDpictureProcess()
        {
            //先创建文件夹，否则删除图片时可能报错
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images", "Portraits");
            Directory.CreateDirectory(imagePath);

            Image result = null;
            DeleteOldPicture();

            photoName = IdUtil.NewId();

            string strPictureBin = Path.Combine(imagePath, $"{photoName}.bin");
            try
            {
                using (FileStream fs = new FileStream(strPictureBin, FileMode.Create, FileAccess.Write))
                {
                    try
                    {
                        fs.Write(CertificateInfo.byPicInfo, 0, CertificateInfo.wPicInfoSize);
                        fs.Flush();
                        fs.Close();
                    }
                    catch (Exception ep)
                    {
                        fs.Close();
                        MessageBox.Show(ep.ToString());
                    }
                }
            }
            catch (Exception ep)
            {
                MessageBox.Show(ep.ToString());
            }
            string strPicture = Path.Combine(imagePath, $"{photoName}.bmp");
            IntPtr StrPath = Marshal.StringToHGlobalAnsi(strPictureBin);

            int num = DSK5022SDK.dewlt(StrPath);
            if (num >= 0)
            {
                result = Image.FromFile(strPicture);
                Marshal.FreeHGlobal(StrPath);
            }
            else
            {
                Marshal.FreeHGlobal(StrPath);
                MessageBox.Show("Fail to acquire the Picture", "Error", MessageBoxButtons.OK);
            }

            return result;
        }

        private void DeleteOldPicture()
        {
            if (string.IsNullOrEmpty(photoName))
            {
                return;
            }
            try
            {
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images", "Portraits");
                string PathPic = Path.Combine(imagePath, $"{photoName}.bmp");
                if (System.IO.File.Exists(PathPic))
                {
                    File.Delete(PathPic);
                }

                string strPictureBin = Path.Combine(imagePath, $"{photoName}.bin");
                if (System.IO.File.Exists(strPictureBin))
                {
                    File.Delete(strPictureBin);
                }

                //清除所有文件
                var files = Directory.GetFiles(imagePath);
                if (!string.IsNullOrEmpty(files?.FirstOrDefault()))
                {
                    foreach (var item in files)
                    {
                        var extension = Path.GetExtension(item);
                        if (extension == ".bin" || extension == ".bmp")
                        {
                            string path = Path.Combine(imagePath, item);
                            File.Delete(path);
                        }
                    }
                }
            }
            catch
            {

            }

        }

    }
}


