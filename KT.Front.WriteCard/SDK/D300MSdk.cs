using System;
using System.Runtime.InteropServices;

namespace KT.Front.WriteCard.SDK
{
    public class D300MSdk
    {
        //TODO 改为配置读取
        public static byte[] Key { get; set; } = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        public static byte Address { get; set; } = 0;
        public static byte RequestMode { get; set; } = 1;
        public static byte AuthMode { get; set; } = 1;
        public static byte BlockAmount { get; set; } = 1;
        public static byte DataLength { get; set; } = 16;

        [DllImport(@"ReferenceFiles\D300MX64\ICReader.dll", EntryPoint = "USBHidExitCommunicate", CharSet = CharSet.Ansi)]
        public static extern int USBHidExitCommunicate();

        [DllImport(@"ReferenceFiles\D300MX64\ICReader.dll", EntryPoint = "USBHidInitCommunicate", CharSet = CharSet.Ansi)]
        public static extern int USBHidInitCommunicate();

        [DllImport(@"ReferenceFiles\D300MX64\ICReader.dll", EntryPoint = "MFHLRead", CharSet = CharSet.Ansi)]
        public static extern int MFHLRead(byte addr, byte requestMode, byte authMode, byte blockAmount, byte startBlock, byte[] key,
              ref IntPtr resCardSerialNum, ref IntPtr cardData, ref IntPtr cardDataLen);

        [DllImport(@"ReferenceFiles\D300MX64\ICReader.dll", EntryPoint = "MFHLWrite")]
        public static extern int MFHLWrite(byte addr, byte requestMode, byte authMode, byte blockAmount, byte startBlock, byte[] key,
                                            byte[] cardData, UInt16 cardDataLen, ref IntPtr resCardSerialNum);
    }
}
