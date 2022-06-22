using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace KT.Quanta.Service.Devices.Schindler
{
    public class SchindlerHelper
    {
        /// <summary>
        /// 获取自增长数字
        /// </summary>
        private static int _databaseNum = 0;
        public static int GetDatabaseMessageId()
        {
            if (_databaseNum >= 255)
            {
                _databaseNum = 0;
            }
            Interlocked.Increment(ref _databaseNum);
            return _databaseNum;
        }

        public static List<XmlDocument> GetXmls(string value)
        {
            var xmls = new List<XmlDocument>();
            using (var reader = new StringReader(value))
            {
                string xmlString = string.Empty;
                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null || line.StartsWith("<?xml"))
                    {
                        if (!string.IsNullOrEmpty(xmlString))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(xmlString);
                            xmls.Add(xml);

                            xmlString = string.Empty;
                        }

                        if (line == null)
                        {
                            break;
                        }
                    }
                    xmlString += line + Environment.NewLine;
                }
            }

            return xmls;
        }
    }
}
