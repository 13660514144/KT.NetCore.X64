using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.SDK
{
    public partial class THPR210SDK
    {
        public static class IDCardApi
        {
            [DllImport("kernel32.dll")]
            public static extern int LoadLibrary(string strDllName);


            #region 常用接口

            /// <summary>
            /// 初始化识别核心
            /// </summary>
            /// <param name="userID">用户 ID，由中安公司提供给客户，用以校验授权</param>
            /// <param name="nType"> 
            /// 每个比特位代表一种识别引擎，比特位为
            /// 1 表示加 载对应的识别引擎，
            /// 0 表示不加载。 
            /// Bit0 代表名片识别核心；
            /// 其他比特位暂未使用，保留。</param>
            /// <param name="lpDirectory">识别核心文件存放的路径。</param>
            /// <returns>
            /// 0 初始化成功
            /// 1 授权 ID 不正确
            /// 2 设备初始化失败
            /// 3 初始化核心失败
            /// 4 未找到授权文件
            /// 5 识别核心加载模板失败
            /// 6 初始化读卡器失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int InitIDCard(char[] userID, int nType, char[] lpDirectory);

            /// <summary>
            /// 释放识别核心。
            /// </summary>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void FreeIDCard();

            /// <summary>
            ///  检测是否有证件放入或拿出
            /// </summary>
            /// <returns>
            /// -1 核心尚未初始化
            /// 0 未检测到证件放入或拿出
            /// 1 检测到有证件放入
            /// 2 检测到证件被拿出
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int DetectDocument();

            /// <summary>
            /// 当检测到放入证件后，调用此接口对证件进行识读.
            /// </summary>
            /// <param name="nCardType"> 
            /// [输出] 
            /// 1 表示证件带有电子芯片
            /// 2 表示证件不带电子芯片
            /// 4 表示检测到条码
            /// 5 表示证件带有电子芯片且检测到条码
            /// 6 表示证件不带电子芯片但检测到条码
            /// </param>
            /// <returns> 
            /// >0 成功返回证件的主类型
            /// -1 没有设置参与自动分类的有效证件类型
            /// -2 图像采集失败
            /// -3 图像裁切失败
            /// -4 分类失败，没有找到匹配的模板
            /// -5 分类失败，没有设置有效证件类型
            /// -6 分类失败，拒识
            /// -7 读卡成功，版面识别失败
            /// -8 读卡失败，版面识别成功
            /// -9 版面识别失败，读芯片失败
            /// 其他 识别失败   
            /// </returns>     
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int AutoProcessIDCard(ref int nCardType);

            /// <summary>
            /// 获取识别字段名称
            /// </summary>
            /// <param name="nAttribute">
            /// 0：获取芯片字段名称 
            /// 1：获取版面 OCR 字段名称 
            /// </param>
            /// <param name="nIndex">字段索引，详见附录 B</param>
            /// <param name="ArrBuffer">存放识别结果的缓冲区</param>
            /// <param name="nBufferLen">
            /// [输入] 缓冲区大小
            /// [输出] 识别结果长度
            /// </param>
            /// <returns>
            /// 0 成功
            /// -1 识别核心未初始化
            /// -2 不存在此属性
            /// 1 缓冲区存储空间太小，nBufferLen 返回需要的空间大小
            /// 2 识别失败
            /// 3 不存在由 nIndex 代表的字段 
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetFieldNameEx(int nAttribute, int nIndex, char[] ArrBuffer, ref int nBufferLen);
            public static extern int GetFieldNameEx(int nAttribute, int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String ArrBuffer, ref int nBufferLen);

            /// <summary>
            /// 获取识别字段内容
            /// </summary>
            /// <param name="nAttribute">
            /// 0：获取芯片字段内容
            /// 1：取版面 OCR 字段内容 
            /// </param>
            /// <param name="nIndex">字段索引，详见附录 B</param>
            /// <param name="lpBuffer">存放识别结果的缓冲区</param>
            /// <param name="nBufferLen">
            /// [输入] 缓冲区大小 
            /// [输出] 识别结果长度
            /// </param>
            /// <returns>
            /// 0 成功
            /// -1 识别核心未初始化
            /// -2 不存在此属性
            /// 1 缓冲区存储空间太小， nBufferLen 返回需要的空间大小
            /// 2 识别失败
            /// 3 不存在由 nIndex 代表的字段
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetRecogResultEx(int nAttribute, int nIndex, char[] lpBuffer, ref int nBufferLen);
            public static extern int GetRecogResultEx(int nAttribute, int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String lpBuffer, ref int nBufferLen);

            /// <summary>
            /// 获取识别结果来源
            /// </summary>
            /// <param name="nAttribute">
            /// 0：获取芯片字段值
            /// 1：取版面 OCR 字段
            /// </param>
            /// <param name="nIndex">nIndex 字段索引，详见附录 B</param>
            /// <returns>
            /// 0 来源为芯片
            /// 1 从 VIZ 区域直接 OCR 出来字段
            /// 2 从 VIZ 导出字段
            /// 3 从 MRZ 区域直接 OCR 出来字段
            /// 4 从 MRZ 导出字段
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetResultTypeEx(int nAttribute, int nIndex);

            /// <summary>
            /// 获取识别结果置信度
            /// </summary>
            /// <param name="nAttribute">
            /// 0：来源为芯片数据
            /// 1：来源为版面 OCR 识别结果 
            /// </param>
            /// <param name="nIndex">字段索引，详见附录 B </param>
            /// <returns>0~100 若置信度不小于 30 ，则认为字段可信。
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetFieldConfEx(int nAttribute, int nIndex);

            /// <summary>
            /// 获取证件名称
            /// 如果设置当前语言为中文，则返回中文证件类型；如果设置当前 语言为英文，则返回英文名称
            /// </summary>
            /// <param name="lpBuffer">用来保存证件名称的 Buffer。 </param>
            /// <param name="nBufferLen">
            /// [输入]lpBuffer 缓冲区的大小；
            /// [输出] 证件名称的长度。
            /// </param>
            /// <returns>
            /// 0 成功
            /// 其他 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetIDCardName(char[] lpBuffer, int nBufferLen);

            /// <summary>
            /// 获取证件子类型
            /// </summary>
            /// <returns>
            /// >0 证件子类型
            /// -1 未获取到证件子类型
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetSubID();

            /// <summary>
            ///  保存指定图像到指定路径
            ///  如果未能成功读取芯片，则无法保存芯片头像。当只保存一种类 型的图像时，保存图像的文件名与参数 lpFileName 一致；
            ///  当保存 多于一种类型的图像时，红外图、紫外图、版面头像和芯片头像分别会在文件名后追加”IR”、 ”UV”、 ”Head”和”HeadEc”，后缀不变。
            /// </summary>
            /// <param name="lpFileName">将图像保存到本地文件的文件名，图像后缀必需为 jpg、bmp 或 tif，其他格式不支持。 </param>
            /// <param name="nType">
            /// 保存图像的类型，bit0~bit4 分别表示白光图、红外图、紫外图、版面头像和芯片头像，
            /// 比特位取值 
            /// 1 表示保存图像，
            /// 0 表示不保存。
            /// </param>
            /// <returns>
            /// 0 成功        
            /// 其他 调用接口失败，bit0 ~bit4 分别表示对应的图像是否 保存成功，比特值 1 表示保存失败，0 表示保存成功。
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SaveImageEx(char[] lpFileName, int nType);

            #endregion

            #region 条码识别

            /// <summary>
            /// 设置条码识别
            /// 如果要识别条码，必须先调用此接口；此接口要在AutoProcessIDCard之前调用
            /// </summary>
            /// <param name="bBarCodeMode">
            /// true 表示识别条码 
            /// false 表示不识别条码 
            /// </param>
            /// <param name="bCellPhoneBarCodeCheck">
            /// true 表示检测手机条码 
            /// false 表示不检测手机条码
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void SetBarCodeMode(bool bBarCodeMode, bool bCellPhoneBarCodeCheck);

            /// <summary>
            ///  获取识别出的条码个数
            /// </summary>
            /// <returns>
            /// >=0 返回条码的个数 
            /// <0 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetBarcodeCount();

            /// <summary>
            /// 获取条码识别结果
            /// </summary>
            /// <param name="nIndex">条码索引，从 0 开始。 </param>
            /// <param name="lpBuffer">条码识别结果 </param>
            /// <param name="nBufferLen">识别结果长度 </param>
            /// <param name="lpResultType">条码类型 </param>
            /// <param name="nResultTypeLen">条码类型长度</param>
            /// <returns>
            /// 0 成功 
            /// -1 lpResult 或 lpResultType 缓冲区太小
            /// -2 识别失败
            /// -3 不存在 nIndex 索引
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetBarcodeRecogResult(int nIndex, char[] lpBuffer, int nBufferLen, char[] lpResultType, int nResultTypeLen);

            #endregion

            #region 常用设置识读选项 有两种方式设置识读选项，一种是通过接口SetConfigByFile导入配置文件， 另一种是通过调用 API 针对不同的识读选项分别进行设置。 使用第一种方式，修改完配置文件后，只需调用接口 SetConfigByFile 即可 一次性导入所有识读选项。配置文件的内容和格式，请参考随安装包附带的配 置文件，位置在安装目录下的 lib 文件夹下，文件名为 IDCardConfig.ini。 使用第二种方式，每个识读选项都需要调用一次 API。

            /// <summary>
            /// 导入配置文件
            /// 从配置文件中加载证件识读选项。
            /// </summary>
            /// <param name="strConfigFile">配置文件的完整路径。 </param>
            /// <returns> 
            /// 0 成功加载配置
            /// -1 读取配置文件出错或配置格式不正确
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetConfigByFile(char[] strConfigFile);

            /// <summary>
            /// 设置语言
            /// 设置识别核心所使用的语言。
            /// </summary>
            /// <param name="nLangType">
            /// 使用语言的类型：
            /// 0 表示中文。
            /// 1 表示英文。
            /// </param>
            /// <returns>
            /// 0 成功
            /// 1 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetLanguage(int nLangType);

            /// <summary>
            /// 设置要保存的图像类型
            /// </summary>
            /// <param name="nImageType">
            /// 设置采集图像的类型，
            /// bit0 到 bit4 分别表示白光图像、红外图像、紫外图像、版面头像和芯片头像， 
            /// 每个比特位取值 1 表示采集对应的图像，0 表示不采集
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void SetSaveImageType(int nImageType);

            /// <summary>
            /// 设置是否进行版面识别
            /// </summary>
            /// <param name="bRecogVIZ">
            /// true 表示进行版面识别 
            /// false 表示不进行版面识别
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void SetRecogVIZ(bool bRecogVIZ);

            /// <summary>
            /// 设置是否读取芯片信息
            /// </summary>
            /// <param name="nDG">
            /// bit1 至 bit16，每个比特位分别表示一个数据分组，
            /// 比特位取值 1 表示读取对应的分组数据，0 表示不读取。 
            /// bit1 表示 DG1； bit2 表示 DG2； ...... bit16 表示 DG16。
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void SetRecogDG(int nDG);

            /// <summary>
            ///  清空参与自动分类的证件类型
            /// </summary>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void ResetIDCardID();

            /// <summary>
            /// 设置参与自动分类的证件类型
            /// 设置/增加参与自动分类的证件 ID。，调用一次只能设置一种证件类型，当要识别多种证件时，可以连续多次调用，进行累加。
            /// </summary>
            /// <param name="nMainID">参数自动分类的证件类型。 </param>
            /// <param name="nSubID">参与自动分类的证件子类型列表。如果 nSubIDCount值为1且nSubID中第一个元素值为0, 则所有子类型都参与自动分类。 </param>
            /// <param name="nSubIdCount">参与自动分类的证件子类型个数。</param>
            /// <returns>
            /// 0 成功
            /// 其他 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int AddIDCardID(int nMainID, int[] nSubID, int nSubIdCount);
            #endregion

            #region  获取设备相关信息
            /// <summary>
            /// 获取设备序列号
            /// </summary>
            /// <param name="ArrSn">用来保存序列号的 Buffer。</param>
            /// <param name="nLength">lpBuffer 缓冲区的大小，最大取值不能超过 16， 建议取值 16。</param>
            /// <returns>
            /// 0 成功
            /// 1 设备未成功加载。
            /// 2 设备不支持此操作。
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetDeviceSN(char[] ArrSn, int nLength);

            /// <summary>
            /// 获取设备型号
            /// </summary>
            /// <param name="lpBuffer">用来保存序列号的 Buffer。</param>
            /// <param name="nLength"> lpBuffer 缓冲区的容量，以 wchar_t 为单位，建议不小于 64。</param>
            /// <returns>
            /// true 成功 
            /// false 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetCurrentDevice(char[] lpBuffer, int nLength);

            #endregion

            #region 其他接口

            /// <summary>
            /// 获取 SDK 版本号
            /// </summary>
            /// <param name="lpBuffer">用来保存识别核心版本号的缓冲区。 </param>
            /// <param name="nLength">lpBuffer 缓冲区的大小。</param> 
            /// <returns>
            /// true 成功 
            /// false 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool GetVersionInfo(char[] lpBuffer, int nLength);

            /// <summary>
            ///  判断设备是否已连接到电脑主机
            /// </summary>
            /// <returns>
            /// 1 设备已经连接到电脑主机并已成功初始化。 
            /// 2 设备掉线（未连接到电脑主机）
            /// 3 设备掉线后重新连接到电脑主机，此时需要重新初始化核心（InitIDCard）
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool CheckDeviceOnline();

            /// <summary>
            /// 
            /// </summary>
            /// <param name="nIOType">
            /// 信号灯编号： 
            /// 5 预备灯 
            /// 6 错误灯 
            /// 7 警告灯 
            /// </param>
            /// <param name="bOpen">
            /// true 表示打开信号灯 
            /// false 表示关闭信号灯
            /// </param>
            /// <returns>
            /// 0 成功 
            /// 1 设备未初始化 
            /// 2 不支持此操作 
            /// 3 参数不合法 
            /// 4 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetIOStatus(int nIOType, bool bOpen);

            /// <summary>
            /// 蜂鸣器报警
            /// </summary>
            /// <param name="nDuration">蜂鸣器鸣响时间长度，以毫秒为单位</param>
            /// <returns>
            /// 0 成功
            /// -1 设备未初始化 
            /// -2 设备不支持此操作
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int BuzzerAlarm(int nDuration);

            /// <summary>
            /// 采集图像到内存
            /// 图像成功采集到内存后，可以通过调用 SaveImageEx 接口将文件 保存到磁盘上。
            /// </summary>
            /// <param name="nImageSizeType">
            /// 证件尺寸： 
            /// 0 代表全幅面的图像，此方式下不进行图像旋转 操作； 
            /// 1 代表一代证类型的图像，即证件的物理尺寸和 放置方式都和一代证相同。此类型包含一代证， 驾照，行驶证。该方式下会自动对证件图像进行 旋转操作； 
            /// 2 代表二代证类型的图像，即证件的物理尺寸和 放置方式都和二代证相同。此类型包含二代证背 面，二代证正面，回乡证正面，回乡证背面，香 港永久性居民身份证。该方式下会自动对证件图 像进行旋转操作； 
            /// 3 代表护照类型的图像 ，即证件的物理尺寸和 放置方式都和护照相同。此类型包含护照，签证， 台胞证，港澳通行证，大陆居民往来台湾通行证。 该方式下会自动对证件图像进行旋转操作； 
            /// 4 代表军官证类型的图像，即证件的物理尺寸和 放置方式都和军官证相同。此类型目前仅包含军 官证。该方式下会自动对证件图像进行旋转操 作； 
            /// 5 户口本类型的图像，即证件的物理尺寸和放置 方式都和户口本相同。此类型目前仅包含户口 本。该方式下会自动对证件图像进行旋转操作； 
            /// 6 登机牌类型的图像。此类型仅含登机牌。该方 式下会自动对证件图像进行旋转操作； 
            /// 7 边民证照片页； 
            /// 8 边民证个人信息页； 
            /// 20 代表自定义类型大小的图像，如果采用此方 式那么需要先设置采集图像的大小，见 3.5.2， 默认情况下自定义类型图像的大小为全幅尺寸， 注意此方式下不会自动进行图像的旋转操作；
            /// 21 代表采集原稿尺寸的图像，该情形下会对采 集图像自动进行去黑边倾斜校正以及剪裁处理， 当使用自动分类功能时，采集图像尺寸使用此方 式。
            /// </param>
            /// <returns>
            /// 0 成功 
            /// 1 失败 
            /// 2 设备不在线 
            /// 3 参数不合法 
            /// 4 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int AcquireImage(int nImageSizeType);
            #endregion

            #region 特殊接口 初始化及释放核心

            /// <summary>
            /// 指定设备型号初始化识别核心
            /// </summary>
            /// <param name="lpUserID">用户 ID，由中安公司提供给客户，用以校验授权。 </param>
            /// <param name="nType">
            /// 每个比特位代表一种识别引擎，
            /// 比特位为 
            /// 1 表示 加载对应的识别引擎，
            /// 0 表示不加载。
            /// Bit0 代表名片识别核心；        
            /// 其他比特位暂未使用，保留。 
            /// </param>
            /// <param name="lpDirectory">本动态库文件存放的路径。 </param>
            /// <param name="lpDeviceName">设备型号</param>
            /// <returns>
            /// 0 初始化成功 
            /// 1 授权 ID 不正确 
            /// 2 设备初始化失败 
            /// 3 初始化核心失败 
            /// 4 未找到授权文件 
            /// 5 识别核心加载模板失败 
            /// 6 初始化读卡器失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int InitIDCardEx(char[] lpUserID, int nType, char[] lpDirectory, char[] lpDeviceName);

            /// <summary>
            /// 指定设备序列号初始化核心
            /// </summary> 
            /// <param name="lpUserID">用户 ID，由中安公司提供给客户，用以校验授权。 </param>
            /// <param name="nType">
            /// 每个比特位代表一种识别引擎，
            /// 比特位为 
            /// 1 表示 加载对应的识别引擎，
            /// 0 表示不加载。
            /// Bit0 代表名片识别核心；        
            /// 其他比特位暂未使用，保留。 
            /// </param>
            /// <param name="lpDirectory">本动态库文件存放的路径。 </param>
            /// <param name="lpDeviceSN">设备序列号</param>
            /// <returns>
            /// 0 初始化成功 
            /// 1 授权 ID 不正确 
            /// 2 设备初始化失败 
            /// 3 初始化核心失败 
            /// 4 未找到授权文件 
            /// 5 识别核心加载模板失败 
            /// 6 初始化读卡器失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int InitIDCardSN(char[] lpUserID, int nType, char[] lpDirectory, char[] lpDeviceSN);

            /// <summary>
            /// 设置拒识标记
            /// 针对某类证件设置拒识标识。
            /// </summary>
            /// <param name="nMainID">证件类型。 </param>
            /// <param name="bSet">是否拒识。</param>
            /// <returns>
            /// 0 成功
            /// 其他 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetIDCardRejectType(int nMainID, bool bSet);

            /// <summary>
            /// 设置是否读取电子芯片
            /// 此接口只针对有读卡功能的设备，在设备初始化之后才能调用此 接口。
            /// </summary>
            /// <param name="nReadCard"> 
            /// 0 表示不读电子芯片； 
            /// 1 表示读取电子芯片。
            /// </param>
            /// <returns>
            /// 0 成功
            /// 其他 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetRecogChipCardAttribute(int nReadCard);

            /// <summary>
            /// 设置是否使用白光图重新识别 MRZ
            /// 按白光图重新识别 MRZ,设置一次即可（用于因红外图机读码不清 晰导致分类失败的情况）
            /// </summary>
            /// <param name="bFlag">
            /// true 设置此功能；
            /// false 表示不设置。
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void ReRecogMRZbyVI(bool bFlag);

            /// <summary>
            /// 设置是否去背景
            /// 适用于 AR、KR 和 QR 无盖情况下，调用此接口可以改善采集图像 效果。
            /// </summary>
            /// <param name="nBGSub">
            /// bit0：白光图是否去除背景 
            /// bit1；红外图是否去除背景 
            /// bit2：紫外图是否去除背景 
            /// 各比特位取值 
            /// 1 表示去除背景，
            /// 0 表示不去除背景
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void SetBGSubtraction(int nBGSub);

            /// <summary>
            /// 设置采集图像的分辨率
            /// 如果客户采集的图像需要识别,请不要修改默认的图像分辨 率，否则会造成识别率下降或者不能识别）.
            /// 仅当客户采集的图像 不需要识别时，才需要调用此接口.
            /// TH-PRXXX 系列设备支持 300 万像素（2048×1536）拍照； 
            /// TH-ARXXX 系列设备和 KR 系列设备支持 300 万像素（2048×1536） 和 500 万像素（2592×1944）拍照。
            /// </summary>
            /// <param name="nResolutionX"> 采集图像的横向分辨率 </param>
            /// <param name="nResolutionY">采集图像的纵向分辨率</param>
            /// <returns>
            /// TRUE 成功 
            /// FALSE 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern bool SetAcquireImageResolution(int nResolutionX, int nResolutionY);

            /// <summary>
            /// 设置采集图像的曝光值
            /// 1. 此接口只支持针对紫外光的设置，参数 nLightType 必需取值为 4； 
            /// 2. 仅适用于 TH-AR 、QR 和 KR 设备，没有特殊情况不要调用
            /// </summary>
            /// <param name="nLightType">必须取值为 4，代表紫外光 </param>
            /// <param name="nModel"> 
            /// 0：出厂设置 
            /// 1：偏暗 
            /// 2：常规 
            /// 3：偏亮
            /// </param> 
            /// <returns>
            /// 0 设置成功
            /// -1 设备不支持此功能
            /// -2 参数错误
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int SetAcquireImageExposureTime(int nLightType, int nModel);

            /// <summary>
            /// 检测证件是否有紫外迟钝特性
            /// </summary>
            /// <param name="bForceAcquire"> 
            /// 1：强制采集紫外图像 
            /// 0：由系统判断是否采集紫外图像 
            /// </param>
            /// <param name="nReserve">
            /// 0 检测到紫外迟钝特征
            /// -1 系统未初始化
            /// -2 设备不支持此功能
            /// -3 采集图像失败
            /// -4 假证件
            /// </param>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern void CheckUVDull(bool bForceAcquire, int nReserve);

            /// <summary>
            /// 检查证件是否有紫外纤维特性
            /// 此功能目前仅适用于护照。
            /// </summary>
            /// <param name="bAcquireImage"> 
            /// true 重新采集图像 
            /// false如果内存中有图像，则不重新采集
            /// </param>
            /// <returns>
            /// <0 检测失败
            /// >=0 检测到紫外纤维的个数
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int FibreDetect(bool bAcquireImage);

            /// <summary>
            /// 获取紫外纤维的位置信息
            /// 此功能目前仅适用于护照，在调用接口 FibreDetect 检测到证件有 紫外纤维特征之后再调用此接口。
            /// </summary>
            /// <param name="nIndex">紫外纤维的索引 </param>
            /// <param name="nLeft">紫外纤维左边像素点 </param>
            /// <param name="nTop">紫外纤维上边像素点 </param>
            /// <param name="nRight">紫外纤维的右边像素点 </param>
            /// <param name="nBottom">紫外纤维的下边像素点</param>
            /// <returns>
            /// 0 成功
            /// 其他 失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetFibrePos(int nIndex, int nLeft, int nTop, int nRight, int nBottom);

            /// <summary>
            ///  检测证件是否为复印件
            ///  目前只支持二代证。
            /// </summary>
            /// <param name="nMainID">证件主 ID</param>
            /// <param name="nScale">
            /// 前 3 个比特位分别表示不同的证件类别，
            /// 比特位取值 
            /// 1 表示进行判别，
            /// 0 表示不进行判别
            /// bit0：复印件判别
            /// bit1：彩色复印件判别
            /// bit2：屏拍件判别返回值
            /// 0 原件
            /// </param>
            /// <param name="bAcquireImage"></param>
            /// <returns>
            /// >0
            /// bit0：是复印件
            /// bit1：是彩色复印件
            /// bit2：是屏拍件
            /// <0 检测失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetImageSourceType(int nMainID, int nScale, bool bAcquireImage);

            #endregion

            #region 其它接口

            /// <summary>
            /// 指定证件类型识别接口
            /// 根据参数中指定的证件类型对图像进行识别。
            /// </summary>
            /// <param name="nMainID">证件主 ID </param>
            /// <param name="nSubID">证件子 ID</param>
            /// <returns>
            /// >0 识别成功，返回值为证件主类型
            /// -1 核心未初始化
            /// -3 分类失败
            /// -4 识别失败
            /// </returns>
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int RecogIDCardEX(int nMainID, int nSubID);

            /// <summary>
            ///  获取电子护照芯片中指定的 DataGroup数据
            /// </summary>
            /// <param name="nDGIndex"> 数据分组索引，1~16 分别代表 DG1~DG16。数据分 组信息详见第六章 附录。 </param>
            /// <param name="bRawData">是否返回从芯片读取的原始数据，true 输出原始信 息，false 输出解析后的信息。 </param>
            /// <param name="lpBuffer"> 用来存储数据分组信息的 Buffer。 </param>
            /// <param name="nLength"> 
            /// [输入] 表示 lpBuffer 的大小； 
            /// [输出] 表示数据分组信息的长度。
            /// </param>
            /// <returns>
            /// 0 成功
            /// 其他 失败
            /// </returns> 
            [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210x64\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            public static extern int GetDataGroupContent(int nDGIndex, bool bRawData, byte[] lpBuffer, ref int nLength);

            #endregion


            //#region 旧接口

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int InitIDCard(char[] cArrUserID, int nType, char[] cArrDirectory);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetRecogResult(int nIndex, char[] cArrBuffer, ref int nBufferLen);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int RecogIDCard();

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetFieldName(int nIndex, char[] cArrBuffer, ref int nBufferLen);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int AcquireImage(int nCardType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int SaveImage(char[] cArrFileName);
            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int SaveHeadImage(char[] cArrFileName);


            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int GetCurrentDevice(char[] cArrDeviceName, int nLength);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern void GetVersionInfo(char[] cArrVersion, int nLength);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern bool CheckDeviceOnline();

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern bool SetAcquireImageType(int nLightType, int nImageType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern bool SetUserDefinedImageSize(int nWidth, int nHeight);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern bool SetAcquireImageResolution(int nResolutionX, int nResolutionY);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int SetIDCardID(int nMainID, int[] nSubID, int nSubIdCount);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int AddIDCardID(int nMainID, int[] nSubID, int nSubIdCount);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int RecogIDCardEX(int nMainID, int nSubID);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetButtonDownType();

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetGrabSignalType();

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int SetSpecialAttribute(int nType, int nSet);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern void FreeIDCard();
            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int GetDeviceSN(char[] cArrSn, int nLength);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetBusinessCardResult(int nID, int nIndex, char[] cArrBuffer, ref int nBufferLen);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int RecogBusinessCard(int nType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetBusinessCardFieldName(int nID, char[] cArrBuffer, ref int nBufferLen);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int GetBusinessCardResultCount(int nID);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int LoadImageToMemory(string path, int nType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int ClassifyIDCard(ref int nCardType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int RecogChipCard(int nDGGroup, bool bRecogVIZ, int nSaveImageType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int RecogGeneralMRZCard(bool bRecogVIZ, int nSaveImageType);

            //[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            //public static extern int RecogCommonCard(int nSaveImageType);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int SaveImageEx(char[] lpFileName, int nType);

            ////[DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
            ////public static extern int GetDataGroupContent(int nDGIndex, bool bRawData, byte[] lpBuffer, ref int len);
            //#endregion

        }









































        //#region 常用接口

        ///// <summary>
        ///// 初始化识别核心
        ///// </summary>
        ///// <param name="userID">用户 ID，由中安公司提供给客户，用以校验授权</param>
        ///// <param name="nType"> 
        ///// 每个比特位代表一种识别引擎，比特位为
        ///// 1 表示加 载对应的识别引擎，
        ///// 0 表示不加载。 
        ///// Bit0 代表名片识别核心；
        ///// 其他比特位暂未使用，保留。</param>
        ///// <param name="lpDirectory">识别核心文件存放的路径。</param>
        ///// <returns>
        ///// 0 初始化成功
        ///// 1 授权 ID 不正确
        ///// 2 设备初始化失败
        ///// 3 初始化核心失败
        ///// 4 未找到授权文件
        ///// 5 识别核心加载模板失败
        ///// 6 初始化读卡器失败
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int InitIDCard([MarshalAs(UnmanagedType.LPWStr)] String userID, int nType, [MarshalAs(UnmanagedType.LPWStr)] String lpDirectory);

        ///// <summary>
        ///// 释放识别核心。
        ///// </summary>
        //delegate void FreeIDCard();

        ///// <summary>
        /////  检测是否有证件放入或拿出
        ///// </summary>
        ///// <returns>
        ///// -1 核心尚未初始化
        ///// 0 未检测到证件放入或拿出
        ///// 1 检测到有证件放入
        ///// 2 检测到证件被拿出</returns>
        //delegate int DetectDocument();

        ///// <summary>
        ///// 当检测到放入证件后，调用此接口对证件进行识读.
        ///// </summary>
        ///// <param name="nCardType"> 
        ///// [输出] 
        ///// 1 表示证件带有电子芯片
        ///// 2 表示证件不带电子芯片
        ///// 4 表示检测到条码
        ///// 5 表示证件带有电子芯片且检测到条码
        ///// 6 表示证件不带电子芯片但检测到条码
        ///// </param>
        ///// <returns> 
        ///// >0 成功返回证件的主类型
        ///// -1 没有设置参与自动分类的有效证件类型
        ///// -2 图像采集失败
        ///// -3 图像裁切失败
        ///// -4 分类失败，没有找到匹配的模板
        ///// -5 分类失败，没有设置有效证件类型
        ///// -6 分类失败，拒识
        ///// -7 读卡成功，版面识别失败
        ///// -8 读卡失败，版面识别成功
        ///// -9 版面识别失败，读芯片失败
        ///// 其他 识别失败   
        ///// </returns>     
        //delegate int AutoProcessIDCard(ref int nCardType);

        ///// <summary>
        ///// 获取识别字段名称
        ///// </summary>
        ///// <param name="nAttribute">
        ///// 0：获取芯片字段名称 
        ///// 1：获取版面 OCR 字段名称 
        ///// </param>
        ///// <param name="nIndex">字段索引，详见附录 B</param>
        ///// <param name="ArrBuffer">存放识别结果的缓冲区</param>
        ///// <param name="nBufferLen">
        ///// [输入] 缓冲区大小
        ///// [输出] 识别结果长度
        ///// </param>
        ///// <returns>
        ///// 0 成功
        ///// -1 识别核心未初始化
        ///// -2 不存在此属性
        ///// 1 缓冲区存储空间太小，nBufferLen 返回需要的空间大小
        ///// 2 识别失败
        ///// 3 不存在由 nIndex 代表的字段 
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int GetFieldNameEx(int nAttribute, int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String ArrBuffer, ref int nBufferLen);

        ///// <summary>
        ///// 获取识别字段内容
        ///// </summary>
        ///// <param name="nAttribute">
        ///// 0：获取芯片字段内容
        ///// 1：取版面 OCR 字段内容 
        ///// </param>
        ///// <param name="nIndex">字段索引，详见附录 B</param>
        ///// <param name="lpBuffer">存放识别结果的缓冲区</param>
        ///// <param name="nBufferLen">
        ///// [输入] 缓冲区大小 
        ///// [输出] 识别结果长度
        ///// </param>
        ///// <returns>
        ///// 0 成功
        ///// -1 识别核心未初始化
        ///// -2 不存在此属性
        ///// 1 缓冲区存储空间太小， nBufferLen 返回需要的空间大小
        ///// 2 识别失败
        ///// 3 不存在由 nIndex 代表的字段
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int GetRecogResultEx(int nAttribute, int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String lpBuffer, ref int nBufferLen);

        ///// <summary>
        ///// 获取识别结果来源
        ///// </summary>
        ///// <param name="nAttribute">
        ///// 0：获取芯片字段值
        ///// 1：取版面 OCR 字段
        ///// </param>
        ///// <param name="nIndex">nIndex 字段索引，详见附录 B</param>
        ///// <returns>
        ///// 0 来源为芯片
        ///// 1 从 VIZ 区域直接 OCR 出来字段
        ///// 2 从 VIZ 导出字段
        ///// 3 从 MRZ 区域直接 OCR 出来字段
        ///// 4 从 MRZ 导出字段
        ///// </returns>
        //delegate int GetResultTypeEx(int nAttribute, int nIndex);

        ///// <summary>
        ///// 获取识别结果置信度
        ///// </summary>
        ///// <param name="nAttribute">
        ///// 0：来源为芯片数据
        ///// 1：来源为版面 OCR 识别结果 
        ///// </param>
        ///// <param name="nIndex">字段索引，详见附录 B </param>
        ///// <returns>0~100 若置信度不小于 30 ，则认为字段可信。</returns>
        //delegate int GetFieldConfEx(int nAttribute, int nIndex);

        ///// <summary>
        ///// 获取证件名称
        ///// 如果设置当前语言为中文，则返回中文证件类型；如果设置当前 语言为英文，则返回英文名称
        ///// </summary>
        ///// <param name="lpBuffer">用来保存证件名称的 Buffer。 </param>
        ///// <param name="nBufferLen">
        ///// [输入]lpBuffer 缓冲区的大小；
        ///// [输出] 证件名称的长度。
        ///// </param>
        ///// <returns>
        ///// 0 成功
        ///// 其他 失败
        ///// </returns>

        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int GetIDCardName([MarshalAs(UnmanagedType.LPWStr)] String lpBuffer, int nBufferLen);

        ///// <summary>
        ///// 获取证件子类型
        ///// </summary>
        ///// <returns>
        ///// >0 证件子类型
        ///// -1 未获取到证件子类型
        ///// </returns>
        //delegate int GetSubID();

        ///// <summary>
        /////  保存指定图像到指定路径
        /////  如果未能成功读取芯片，则无法保存芯片头像。当只保存一种类 型的图像时，保存图像的文件名与参数 lpFileName 一致；
        /////  当保存 多于一种类型的图像时，红外图、紫外图、版面头像和芯片头像分别会在文件名后追加”IR”、 ”UV”、 ”Head”和”HeadEc”，后缀不变。
        ///// </summary>
        ///// <param name="lpFileName">将图像保存到本地文件的文件名，图像后缀必需为 jpg、bmp 或 tif，其他格式不支持。 </param>
        ///// <param name="nType">
        ///// 保存图像的类型，bit0~bit4 分别表示白光图、红外图、紫外图、版面头像和芯片头像，
        ///// 比特位取值 
        ///// 1 表示保存图像，
        ///// 0 表示不保存。
        ///// </param>
        ///// <returns>
        ///// 0 成功        
        ///// 其他 调用接口失败，bit0 ~bit4 分别表示对应的图像是否 保存成功，比特值 1 表示保存失败，0 表示保存成功。
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int SaveImageEx([MarshalAs(UnmanagedType.LPWStr)] String lpFileName, int nType);

        //#endregion

        //#region 条码识别

        ///// <summary>
        ///// 设置条码识别
        ///// 如果要识别条码，必须先调用此接口；此接口要在AutoProcessIDCard之前调用
        ///// </summary>
        ///// <param name="bBarCodeMode">
        ///// true 表示识别条码 
        ///// false 表示不识别条码 
        ///// </param>
        ///// <param name="bCellPhoneBarCodeCheck">
        ///// true 表示检测手机条码 
        ///// false 表示不检测手机条码
        ///// </param>
        //delegate void SetBarCodeMode(bool bBarCodeMode, bool bCellPhoneBarCodeCheck);

        ///// <summary>
        /////  获取识别出的条码个数
        ///// </summary>
        ///// <returns>
        ///// >=0 返回条码的个数 
        ///// <0 失败
        ///// </returns>
        //delegate int GetBarcodeCount();

        ///// <summary>
        ///// 获取条码识别结果
        ///// </summary>
        ///// <param name="nIndex">条码索引，从 0 开始。 </param>
        ///// <param name="lpBuffer">条码识别结果 </param>
        ///// <param name="nBufferLen">识别结果长度 </param>
        ///// <param name="lpResultType">条码类型 </param>
        ///// <param name="nResultTypeLen">条码类型长度</param>
        ///// <returns>
        ///// 0 成功 
        ///// -1 lpResult 或 lpResultType 缓冲区太小
        ///// -2 识别失败
        ///// -3 不存在 nIndex 索引
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int GetBarcodeRecogResult(int nIndex, [MarshalAs(UnmanagedType.LPWStr)] String lpBuffer, int nBufferLen, [MarshalAs(UnmanagedType.LPWStr)] String lpResultType, int nResultTypeLen);

        //#endregion

        //#region 常用设置识读选项 有两种方式设置识读选项，一种是通过接口SetConfigByFile导入配置文件， 另一种是通过调用 API 针对不同的识读选项分别进行设置。 使用第一种方式，修改完配置文件后，只需调用接口 SetConfigByFile 即可 一次性导入所有识读选项。配置文件的内容和格式，请参考随安装包附带的配 置文件，位置在安装目录下的 lib 文件夹下，文件名为 IDCardConfig.ini。 使用第二种方式，每个识读选项都需要调用一次 API。

        ///// <summary>
        ///// 导入配置文件
        ///// 从配置文件中加载证件识读选项。
        ///// </summary>
        ///// <param name="strConfigFile">配置文件的完整路径。 </param>
        ///// <returns> 
        ///// 0 成功加载配置
        ///// -1 读取配置文件出错或配置格式不正确
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int SetConfigByFile([MarshalAs(UnmanagedType.LPWStr)] String strConfigFile);

        ///// <summary>
        ///// 设置语言
        ///// 设置识别核心所使用的语言。
        ///// </summary>
        ///// <param name="nLangType">
        ///// 使用语言的类型：
        ///// 0 表示中文。
        ///// 1 表示英文。
        ///// </param>
        ///// <returns>
        ///// 0 成功
        ///// 1 失败
        ///// </returns>
        //delegate int SetLanguage(int nLangType);

        ///// <summary>
        ///// 设置要保存的图像类型
        ///// </summary>
        ///// <param name="nImageType">
        ///// 设置采集图像的类型，
        ///// bit0 到 bit4 分别表示白光图像、红外图像、紫外图像、版面头像和芯片头像， 
        ///// 每个比特位取值 1 表示采集对应的图像，0 表示不采集
        ///// </param>
        //delegate void SetSaveImageType(int nImageType);

        ///// <summary>
        ///// 设置是否进行版面识别
        ///// </summary>
        ///// <param name="bRecogVIZ">
        ///// true 表示进行版面识别 
        ///// false 表示不进行版面识别
        ///// </param>
        //delegate void SetRecogVIZ(bool bRecogVIZ);

        ///// <summary>
        ///// 设置是否读取芯片信息
        ///// </summary>
        ///// <param name="nDG">
        ///// bit1 至 bit16，每个比特位分别表示一个数据分组，
        ///// 比特位取值 1 表示读取对应的分组数据，0 表示不读取。 
        ///// bit1 表示 DG1； bit2 表示 DG2； ...... bit16 表示 DG16。
        ///// </param>
        //delegate void SetRecogDG(int nDG);

        ///// <summary>
        /////  清空参与自动分类的证件类型
        ///// </summary>
        //delegate void ResetIDCardID();

        ///// <summary>
        ///// 设置参与自动分类的证件类型
        ///// 设置/增加参与自动分类的证件 ID。，调用一次只能设置一种证件类型，当要识别多种证件时，可以连续多次调用，进行累加。
        ///// </summary>
        ///// <param name="nMainID">参数自动分类的证件类型。 </param>
        ///// <param name="nSubID">参与自动分类的证件子类型列表。如果 nSubIDCount值为1且nSubID中第一个元素值为0, 则所有子类型都参与自动分类。 </param>
        ///// <param name="nSubIdCount">参与自动分类的证件子类型个数。</param>
        ///// <returns>
        ///// 0 成功
        ///// 其他 失败
        ///// </returns>
        //delegate int AddIDCardID(int nMainID, int[] nSubID, int nSubIdCount);
        //#endregion

        //#region  获取设备相关信息
        ///// <summary>
        ///// 获取设备序列号
        ///// </summary>
        ///// <param name="ArrSn">用来保存序列号的 Buffer。</param>
        ///// <param name="nLength">lpBuffer 缓冲区的大小，最大取值不能超过 16， 建议取值 16。</param>
        ///// <returns>
        ///// 0 成功
        ///// 1 设备未成功加载。
        ///// 2 设备不支持此操作。
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int GetDeviceSN([MarshalAs(UnmanagedType.LPWStr)] String ArrSn, int nLength);

        ///// <summary>
        ///// 获取设备型号
        ///// </summary>
        ///// <param name="lpBuffer">用来保存序列号的 Buffer。</param>
        ///// <param name="nLength"> lpBuffer 缓冲区的容量，以 wchar_t 为单位，建议不小于 64。</param>
        ///// <returns>
        ///// true 成功 
        ///// false 失败
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int GetCurrentDevice([MarshalAs(UnmanagedType.LPWStr)] String lpBuffer, int nLength);

        //#endregion

        //#region 其他接口

        ///// <summary>
        ///// 获取 SDK 版本号
        ///// </summary>
        ///// <param name="lpBuffer">用来保存识别核心版本号的缓冲区。 </param>
        ///// <param name="nLength">lpBuffer 缓冲区的大小。</param> 
        ///// <returns>
        ///// true 成功 
        ///// false 失败
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate bool GetVersionInfo([MarshalAs(UnmanagedType.LPWStr)] String lpBuffer, int nLength);

        ///// <summary>
        /////  判断设备是否已连接到电脑主机
        ///// </summary>
        ///// <returns>
        ///// 1 设备已经连接到电脑主机并已成功初始化。 
        ///// 2 设备掉线（未连接到电脑主机）
        ///// 3 设备掉线后重新连接到电脑主机，此时需要重新初始化核心（InitIDCard）
        ///// </returns>
        //delegate bool CheckDeviceOnline();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="nIOType">
        ///// 信号灯编号： 
        ///// 5 预备灯 
        ///// 6 错误灯 
        ///// 7 警告灯 
        ///// </param>
        ///// <param name="bOpen">
        ///// true 表示打开信号灯 
        ///// false 表示关闭信号灯
        ///// </param>
        ///// <returns>
        ///// 0 成功 
        ///// 1 设备未初始化 
        ///// 2 不支持此操作 
        ///// 3 参数不合法 
        ///// 4 失败
        ///// </returns>
        //delegate int SetIOStatus(int nIOType, bool bOpen);

        ///// <summary>
        ///// 蜂鸣器报警
        ///// </summary>
        ///// <param name="nDuration">蜂鸣器鸣响时间长度，以毫秒为单位</param>
        ///// <returns>
        ///// 0 成功
        ///// -1 设备未初始化 
        ///// -2 设备不支持此操作
        ///// </returns>
        //delegate int BuzzerAlarm(int nDuration);

        ///// <summary>
        ///// 采集图像到内存
        ///// 图像成功采集到内存后，可以通过调用 SaveImageEx 接口将文件 保存到磁盘上。
        ///// </summary>
        ///// <param name="nImageSizeType">
        ///// 证件尺寸： 
        ///// 0 代表全幅面的图像，此方式下不进行图像旋转 操作； 
        ///// 1 代表一代证类型的图像，即证件的物理尺寸和 放置方式都和一代证相同。此类型包含一代证， 驾照，行驶证。该方式下会自动对证件图像进行 旋转操作； 
        ///// 2 代表二代证类型的图像，即证件的物理尺寸和 放置方式都和二代证相同。此类型包含二代证背 面，二代证正面，回乡证正面，回乡证背面，香 港永久性居民身份证。该方式下会自动对证件图 像进行旋转操作； 
        ///// 3 代表护照类型的图像 ，即证件的物理尺寸和 放置方式都和护照相同。此类型包含护照，签证， 台胞证，港澳通行证，大陆居民往来台湾通行证。 该方式下会自动对证件图像进行旋转操作； 
        ///// 4 代表军官证类型的图像，即证件的物理尺寸和 放置方式都和军官证相同。此类型目前仅包含军 官证。该方式下会自动对证件图像进行旋转操 作； 
        ///// 5 户口本类型的图像，即证件的物理尺寸和放置 方式都和户口本相同。此类型目前仅包含户口 本。该方式下会自动对证件图像进行旋转操作； 
        ///// 6 登机牌类型的图像。此类型仅含登机牌。该方 式下会自动对证件图像进行旋转操作； 
        ///// 7 边民证照片页； 
        ///// 8 边民证个人信息页； 
        ///// 20 代表自定义类型大小的图像，如果采用此方 式那么需要先设置采集图像的大小，见 3.5.2， 默认情况下自定义类型图像的大小为全幅尺寸， 注意此方式下不会自动进行图像的旋转操作；
        ///// 21 代表采集原稿尺寸的图像，该情形下会对采 集图像自动进行去黑边倾斜校正以及剪裁处理， 当使用自动分类功能时，采集图像尺寸使用此方 式。
        ///// </param>
        ///// <returns>
        ///// 0 成功 
        ///// 1 失败 
        ///// 2 设备不在线 
        ///// 3 参数不合法 
        ///// 4 失败
        ///// </returns>
        //delegate int AcquireImage(int nImageSizeType);
        //#endregion

        //#region 特殊接口 初始化及释放核心

        ///// <summary>
        ///// 指定设备型号初始化识别核心
        ///// </summary>
        ///// <param name="lpUserID">用户 ID，由中安公司提供给客户，用以校验授权。 </param>
        ///// <param name="nType">
        ///// 每个比特位代表一种识别引擎，
        ///// 比特位为 
        ///// 1 表示 加载对应的识别引擎，
        ///// 0 表示不加载。
        ///// Bit0 代表名片识别核心；        
        ///// 其他比特位暂未使用，保留。 
        ///// </param>
        ///// <param name="lpDirectory">本动态库文件存放的路径。 </param>
        ///// <param name="lpDeviceName">设备型号</param>
        ///// <returns>
        ///// 0 初始化成功 
        ///// 1 授权 ID 不正确 
        ///// 2 设备初始化失败 
        ///// 3 初始化核心失败 
        ///// 4 未找到授权文件 
        ///// 5 识别核心加载模板失败 
        ///// 6 初始化读卡器失败
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int InitIDCardEx([MarshalAs(UnmanagedType.LPWStr)] String lpUserID, int nType, [MarshalAs(UnmanagedType.LPWStr)] String lpDirectory, [MarshalAs(UnmanagedType.LPWStr)] String lpDeviceName);

        ///// <summary>
        ///// 指定设备序列号初始化核心
        ///// </summary> 
        ///// <param name="lpUserID">用户 ID，由中安公司提供给客户，用以校验授权。 </param>
        ///// <param name="nType">
        ///// 每个比特位代表一种识别引擎，
        ///// 比特位为 
        ///// 1 表示 加载对应的识别引擎，
        ///// 0 表示不加载。
        ///// Bit0 代表名片识别核心；        
        ///// 其他比特位暂未使用，保留。 
        ///// </param>
        ///// <param name="lpDirectory">本动态库文件存放的路径。 </param>
        ///// <param name="lpDeviceSN">设备序列号</param>
        ///// <returns>
        ///// 0 初始化成功 
        ///// 1 授权 ID 不正确 
        ///// 2 设备初始化失败 
        ///// 3 初始化核心失败 
        ///// 4 未找到授权文件 
        ///// 5 识别核心加载模板失败 
        ///// 6 初始化读卡器失败
        ///// </returns>
        //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        //delegate int InitIDCardSN([MarshalAs(UnmanagedType.LPWStr)] String lpUserID, int nType, [MarshalAs(UnmanagedType.LPWStr)] String lpDirectory, [MarshalAs(UnmanagedType.LPWStr)] String lpDeviceSN);

        ///// <summary>
        ///// 设置拒识标记
        ///// 针对某类证件设置拒识标识。
        ///// </summary>
        ///// <param name="nMainID">证件类型。 </param>
        ///// <param name="bSet">是否拒识。</param>
        ///// <returns>
        ///// 0 成功
        ///// 其他 失败
        ///// </returns>
        //delegate int SetIDCardRejectType(int nMainID, bool bSet);

        ///// <summary>
        ///// 设置是否读取电子芯片
        ///// 此接口只针对有读卡功能的设备，在设备初始化之后才能调用此 接口。
        ///// </summary>
        ///// <param name="nReadCard"> 
        ///// 0 表示不读电子芯片； 
        ///// 1 表示读取电子芯片。
        ///// </param>
        ///// <returns>
        ///// 0 成功
        ///// 其他 失败
        ///// </returns>
        //delegate int SetRecogChipCardAttribute(int nReadCard);

        ///// <summary>
        ///// 设置是否使用白光图重新识别 MRZ
        ///// 按白光图重新识别 MRZ,设置一次即可（用于因红外图机读码不清 晰导致分类失败的情况）
        ///// </summary>
        ///// <param name="bFlag">
        ///// true 设置此功能；
        ///// false 表示不设置。
        ///// </param>
        //delegate void ReRecogMRZbyVI(bool bFlag);

        ///// <summary>
        ///// 设置是否去背景
        ///// 适用于 AR、KR 和 QR 无盖情况下，调用此接口可以改善采集图像 效果。
        ///// </summary>
        ///// <param name="nBGSub">
        ///// bit0：白光图是否去除背景 
        ///// bit1；红外图是否去除背景 
        ///// bit2：紫外图是否去除背景 
        ///// 各比特位取值 
        ///// 1 表示去除背景，
        ///// 0 表示不去除背景
        ///// </param>
        //delegate void SetBGSubtraction(int nBGSub);

        ///// <summary>
        ///// 设置采集图像的分辨率
        ///// 如果客户采集的图像需要识别,请不要修改默认的图像分辨 率，否则会造成识别率下降或者不能识别）.
        ///// 仅当客户采集的图像 不需要识别时，才需要调用此接口.
        ///// TH-PRXXX 系列设备支持 300 万像素（2048×1536）拍照； 
        ///// TH-ARXXX 系列设备和 KR 系列设备支持 300 万像素（2048×1536） 和 500 万像素（2592×1944）拍照。
        ///// </summary>
        ///// <param name="nResolutionX"> 采集图像的横向分辨率 </param>
        ///// <param name="nResolutionY">采集图像的纵向分辨率</param>
        ///// <returns>
        ///// TRUE 成功 
        ///// FALSE 失败
        ///// </returns>
        //delegate bool SetAcquireImageResolution(int nResolutionX, int nResolutionY);

        ///// <summary>
        ///// 设置采集图像的曝光值
        ///// 1. 此接口只支持针对紫外光的设置，参数 nLightType 必需取值为 4； 
        ///// 2. 仅适用于 TH-AR 、QR 和 KR 设备，没有特殊情况不要调用
        ///// </summary>
        ///// <param name="nLightType">必须取值为 4，代表紫外光 </param>
        ///// <param name="nModel"> 
        ///// 0：出厂设置 
        ///// 1：偏暗 
        ///// 2：常规 
        ///// 3：偏亮
        ///// </param> 
        ///// <returns>
        ///// 0 设置成功
        ///// -1 设备不支持此功能
        ///// -2 参数错误
        ///// </returns>
        //delegate int SetAcquireImageExposureTime(int nLightType, int nModel);

        ///// <summary>
        ///// 检测证件是否有紫外迟钝特性
        ///// </summary>
        ///// <param name="bForceAcquire"> 
        ///// 1：强制采集紫外图像 
        ///// 0：由系统判断是否采集紫外图像 
        ///// </param>
        ///// <param name="nReserve">
        ///// 0 检测到紫外迟钝特征
        ///// -1 系统未初始化
        ///// -2 设备不支持此功能
        ///// -3 采集图像失败
        ///// -4 假证件
        ///// </param>
        //delegate void CheckUVDull(bool bForceAcquire, int nReserve);

        ///// <summary>
        ///// 检查证件是否有紫外纤维特性
        ///// 此功能目前仅适用于护照。
        ///// </summary>
        ///// <param name="bAcquireImage"> 
        ///// true 重新采集图像 
        ///// false如果内存中有图像，则不重新采集
        ///// </param>
        ///// <returns>
        ///// <0 检测失败
        ///// >=0 检测到紫外纤维的个数
        ///// </returns>
        //delegate int FibreDetect(bool bAcquireImage);

        ///// <summary>
        ///// 获取紫外纤维的位置信息
        ///// 此功能目前仅适用于护照，在调用接口 FibreDetect 检测到证件有 紫外纤维特征之后再调用此接口。
        ///// </summary>
        ///// <param name="nIndex">紫外纤维的索引 </param>
        ///// <param name="nLeft">紫外纤维左边像素点 </param>
        ///// <param name="nTop">紫外纤维上边像素点 </param>
        ///// <param name="nRight">紫外纤维的右边像素点 </param>
        ///// <param name="nBottom">紫外纤维的下边像素点</param>
        ///// <returns>
        ///// 0 成功
        ///// 其他 失败
        ///// </returns>
        //delegate int GetFibrePos(int nIndex, int nLeft, int nTop, int nRight, int nBottom);

        ///// <summary>
        /////  检测证件是否为复印件
        /////  目前只支持二代证。
        ///// </summary>
        ///// <param name="nMainID">证件主 ID</param>
        ///// <param name="nScale">
        ///// 前 3 个比特位分别表示不同的证件类别，
        ///// 比特位取值 
        ///// 1 表示进行判别，
        ///// 0 表示不进行判别
        ///// bit0：复印件判别
        ///// bit1：彩色复印件判别
        ///// bit2：屏拍件判别返回值
        ///// 0 原件
        ///// </param>
        ///// <param name="bAcquireImage"></param>
        ///// <returns>
        ///// >0
        ///// bit0：是复印件
        ///// bit1：是彩色复印件
        ///// bit2：是屏拍件
        ///// <0 检测失败
        ///// </returns>
        //delegate int GetImageSourceType(int nMainID, int nScale, bool bAcquireImage);

        //#endregion

        //#region 其它接口

        ///// <summary>
        ///// 指定证件类型识别接口
        ///// 根据参数中指定的证件类型对图像进行识别。
        ///// </summary>
        ///// <param name="nMainID">证件主 ID </param>
        ///// <param name="nSubID">证件子 ID</param>
        ///// <returns>
        ///// >0 识别成功，返回值为证件主类型
        ///// -1 核心未初始化
        ///// -3 分类失败
        ///// -4 识别失败
        ///// </returns>
        //delegate int RecogIDCardEX(int nMainID, int nSubID);

        ///// <summary>
        /////  获取电子护照芯片中指定的 DataGroup数据
        ///// </summary>
        ///// <param name="nDGIndex"> 数据分组索引，1~16 分别代表 DG1~DG16。数据分 组信息详见第六章 附录。 </param>
        ///// <param name="bRawData">是否返回从芯片读取的原始数据，true 输出原始信 息，false 输出解析后的信息。 </param>
        ///// <param name="lpBuffer"> 用来存储数据分组信息的 Buffer。 </param>
        ///// <param name="nLength"> 
        ///// [输入] 表示 lpBuffer 的大小； 
        ///// [输出] 表示数据分组信息的长度。
        ///// </param>
        ///// <returns>
        ///// 0 成功
        ///// 其他 失败
        ///// </returns>
        //delegate int GetDataGroupContent(int nDGIndex, bool bRawData, char[] lpBuffer, int nLength);

        //#endregion

        //public static class IDCardApi
        //{
        //    [DllImport("kernel32.dll")]
        //    public static extern int LoadLibrary(string strDllName);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int InitIDCard(char[] cArrUserID, int nType, char[] cArrDirectory);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetRecogResult(int nIndex, char[] cArrBuffer, ref int nBufferLen);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int RecogIDCard();

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetFieldName(int nIndex, char[] cArrBuffer, ref int nBufferLen);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int AcquireImage(int nCardType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int SaveImage(char[] cArrFileName);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int SaveHeadImage(char[] cArrFileName);


        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetCurrentDevice(char[] cArrDeviceName, int nLength);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern void GetVersionInfo(char[] cArrVersion, int nLength);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern bool CheckDeviceOnline();

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern bool SetAcquireImageType(int nLightType, int nImageType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern bool SetUserDefinedImageSize(int nWidth, int nHeight);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern bool SetAcquireImageResolution(int nResolutionX, int nResolutionY);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int SetIDCardID(int nMainID, int[] nSubID, int nSubIdCount);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int AddIDCardID(int nMainID, int[] nSubID, int nSubIdCount);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int RecogIDCardEX(int nMainID, int nSubID);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetButtonDownType();

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetGrabSignalType();

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int SetSpecialAttribute(int nType, int nSet);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern void FreeIDCard();
        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetDeviceSN(char[] cArrSn, int nLength);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetBusinessCardResult(int nID, int nIndex, char[] cArrBuffer, ref int nBufferLen);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int RecogBusinessCard(int nType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetBusinessCardFieldName(int nID, char[] cArrBuffer, ref int nBufferLen);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetBusinessCardResultCount(int nID);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int LoadImageToMemory(string path, int nType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int ClassifyIDCard(ref int nCardType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int RecogChipCard(int nDGGroup, bool bRecogVIZ, int nSaveImageType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int RecogGeneralMRZCard(bool bRecogVIZ, int nSaveImageType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int RecogCommonCard(int nSaveImageType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int SaveImageEx(char[] lpFileName, int nType);

        //    [DllImport("ReferenceFiles\\IdReaderSdks\\THPR210\\IDCard.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        //    public static extern int GetDataGroupContent(int nDGIndex, bool bRawData, byte[] lpBuffer, ref int len);
        //}





    }
}
