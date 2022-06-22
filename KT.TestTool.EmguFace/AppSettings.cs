using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.TestTool.EmguFace
{
    public class AppSettings
    {
        /// <summary>
        /// Haar人脸识别文件
        /// </summary>
        public string PartHaarCascadePath { get; set; } = Path.Combine("files", "haarcascades", "haarcascade_frontalface_default.xml");

    }
}
