using Emgu.CV;
using System;

namespace KT.Unit.Face.Arc.Free.SDKUtil
{
    /// <summary>
    /// The inteface for reading a file into a Mat
    /// </summary>
    public interface IFileReaderMat
    {
        /// <summary>
        /// Read the file into a Mat
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="mat">The destination mat</param>
        /// <param name="loadType">The image loading type</param>
        /// <returns>True if successful</returns>
        bool ReadFile(String fileName, Mat mat, Emgu.CV.CvEnum.ImreadModes loadType);
    }
}
