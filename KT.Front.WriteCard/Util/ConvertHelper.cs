namespace KT.Front.WriteCard.Util
{
    public class ConvertHelper
    {
        /// <summary>
        /// 获取高字节
        /// </summary>
        /// <param name="intval"></param>
        /// <returns></returns>
        public static int GetHighWord(int intval)
        {
            return ((intval % 65536) / 256);
        }

        /// <summary>
        /// 获取低字节
        /// </summary>
        /// <param name="intval"></param>
        /// <returns></returns>
        public static int GetLowWord(int intval)
        {
            return (intval % 256);
        }
    }
}
