using IDevices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.Common
{
    /// <summary>
    /// 证件阅读器公共类
    /// </summary>
    public class Reader : IReader
    {
        //设备
        private IDevice _device;
        private IDevice _scanDevice;
        private bool _isReadAndScan;
        private bool _isEveryConnect;

        private ILogger<Reader> _logger;
        private IdReaderSettings _idReaderSettings;

        public Reader(ILogger<Reader> logger)
        {
            _logger = logger;
        }

        public Reader(ILogger<Reader> logger, IdReaderSettings idReaderSettings)
        {
            _logger = logger;
            _idReaderSettings = idReaderSettings;
        }

        public void Init(IDevice device, IDevice scanDevice = null, bool isReadAndScan = false, bool isEveryConnect = false,string ScanType="")
        {
            _device = device;
            _scanDevice = scanDevice;
            _isReadAndScan = isReadAndScan;
            _isEveryConnect = isEveryConnect;

            if (!_isEveryConnect)
            {
                if (device != null)
                {
                    LogDebug("开始{0}阅读器连接！", _device.ReaderType.Value);
                    //开启连接
                    _device.InitComm();
                    LogDebug("{0}阅读器连接完成！", _device.ReaderType.Value);
                }
                if (_scanDevice != null)
                {
                    LogDebug("开始{0}扫描器连接！", _scanDevice.ReaderType.Value);
                    //开启连接
                    _scanDevice.InitComm();
                    LogDebug("{0}扫描器连接完成！", _scanDevice.ReaderType.Value);
                }
            }
            else
            {
                _device.StartSignalLamp();
            }
        }

        public void Close()
        {
            if (_device != null)
            {
                LogDebug("开始{0}阅读器关闭！", _device.ReaderType.Value);
                //开启关闭
                _device.CloseComm();
                LogDebug("{0}阅读器关闭完成！", _device.ReaderType.Value);
            }
            if (_scanDevice != null)
            {
                LogDebug("开始{0}阅读器关闭！", _scanDevice.ReaderType.Value);
                //开启关闭
                _scanDevice.CloseComm();
                LogDebug("{0}阅读器关闭完成！", _scanDevice?.ReaderType?.Value);
            }
        }

        public string GetReaderType()
        {
            return _device?.ReaderType?.Value;
        }

        private void LogDebug(string message, params Object[] args)
        {
            if (_idReaderSettings?.IsLogDebugIdReader == true)
            {
                _logger.LogDebug(message, args);
            }
        }
        /// <summary>
        /// 读卡器
        /// </summary>
        /// <returns></returns>
        public async Task<Person> ReadAsync()
        {
            if (_isEveryConnect)
            {
                var person = ExecuteEveryRead();
                _logger.LogInformation($"person:{JsonConvert.SerializeObject(person)}");
                if (person != null)
                {
                    return person;
                }
                if (_isReadAndScan)
                {
                    person = await ExecuteEveryScanAsync(null);
                }
                return person;
            }
            else
            {
                var person = ExecuteRead();
                if (person != null)
                {                    
                    return person;
                }
                if (_isReadAndScan)
                {
                    //_logger.LogInformation("run=ReadAsync para null");
                    person = await ExecuteScanAsync(null);
                }
                /*只要放入证件 烧苗成功*/
                //_logger.LogInformation($"run=ReadAsync para null person={JsonConvert.SerializeObject(person)}");
                return person;
            }
        }
        /// <summary>
        /// 扫描仪
        /// </summary>
        /// <param name="operateIdType"></param>
        /// <returns></returns>
        public async Task<Person> ScanAsync(string operateIdType)
        {
            if (_isEveryConnect)
            {
               // _logger.LogInformation($"run=ScanAsync para={operateIdType}");
                return await ExecuteScanAsync(operateIdType);
            }
            else
            {
                return await ExecuteEveryScanAsync(operateIdType);
            }
        }

        /// <summary>
        /// 扫描身份证件  证件阅读器
        /// </summary>
        /// <returns>从身份证件获取的人员信息</returns>
        public Person ExecuteRead()
        {
            Person person;
            try
            {
                //LogDebug("开始{0}阅读器连接！", _device.ReaderType.Value);
                try
                {
                    //LogDebug("开始{0}阅读器认证！", _device.ReaderType.Value);
                    //认证
                    _device.Authenticate();
                    LogDebug("开始{0}阅读器读取内容！", _device.ReaderType.Value);
                    //读取内容
                    person = _device.ReadContent();
                }
                catch (IdReaderException ex)
                {
                    LogDebug(ex.Message);
                    person = null;
                }
                catch (Exception ex)
                {
                    _logger.LogError("扫描身份证件错误：ex:{0} ", ex);
                    person = null;
                }
            }
            catch (IdReaderException ex)
            {
                LogDebug(ex.Message);
                person = null;
            }
            catch (Exception ex)
            {
                _logger.LogError("扫描{0}身份证件错误：ex:{1} ", _device.ReaderType.Value, ex);
                person = null;
            }
            //LogDebug("结束{0}阅读器！", _device.ReaderType.Value);
            if (person == null)
            {
                return null;
            }
            //LogDebug("阅读器{0}获取结果 Run=ExecuteRead：{1} ", _device.ReaderType.Value, JsonConvert.SerializeObject(person));
            _logger.LogInformation(JsonConvert.SerializeObject($"person:{person}"));
            return person;
        }
        /// <summary>
        /// 执行扫描仪操作  自动烧苗
        /// </summary>
        /// <param name="operateIdType"></param>
        /// <returns></returns>
        public async Task<Person> ExecuteScanAsync(string operateIdType)
        {
            return await Task.Run(() =>
            {
                Person person = null;
                try
                {
                    //LogDebug("开始{0}执行扫描仪操作连接！", _scanDevice.ReaderType.Value);
                    try
                    {
                        //LogDebug("开始{0}执行扫描仪认证！", _scanDevice.ReaderType.Value);
                        //认证
                        _scanDevice.Authenticate();
                        //LogDebug("开始{0}执行扫描仪读取内容！", _scanDevice.ReaderType.Value);
                        //读取内容 
                        person = _scanDevice.ScanContent(operateIdType);
                    }
                    catch (IdReaderException ex)
                    {
                        LogDebug(ex.Message);
                        person = null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("扫描{0}证件错误：ex:{1} ", _scanDevice.ReaderType.Value, ex);
                        person = null;
                    }
                }
                catch (IdReaderException ex)
                {
                    LogDebug(ex.Message);
                    person = null;
                }
                catch (Exception ex)
                {
                    _logger.LogError("扫描{0}证件错误：ex:{1} ", _scanDevice.ReaderType.Value, ex);
                    person = null;
                }
                //LogDebug("结束{0}执行扫描仪！", _scanDevice.ReaderType.Value);
                if (person == null)
                {
                    return null;
                }
                //LogDebug("执行扫描仪{0}获取结果：Run=ExecuteScanAsync  {1} ", _scanDevice.ReaderType.Value, JsonConvert.SerializeObject(person));
                return person;
            });
        }

        /// <summary>
        /// 扫描身份证件
        /// </summary>
        /// <returns>从身份证件获取的人员信息</returns>
        public Person ExecuteEveryRead()
        {
            Person person = null;
            try
            {
                //LogDebug("开始{0}阅读器连接！", _device.ReaderType.Value);

                //开启连接
                _device.InitComm();

                try
                {
                    //LogDebug("开始{0}阅读器认证！", _device.ReaderType.Value);
                    //认证
                    _device.Authenticate();
                    LogDebug("开始{0}阅读器读取内容！", _device.ReaderType.Value);
                    //读取内容
                    person = _device.ReadContent();
                }
                catch (IdReaderException ex)
                {
                    LogDebug(ex.Message);
                    person = null;
                }
                catch (Exception ex)
                {
                    _logger.LogError("扫描身份证件错误：ex:{0} ", ex);
                    person = null;
                }
                finally
                {
                    //LogDebug("开始{0}阅读器关闭连接！", _device.ReaderType.Value);
                    //关闭连接
                    _device.CloseComm();
                }
            }
            catch (IdReaderException ex)
            {
                LogDebug(ex.Message);
                person = null;
            }
            catch (Exception ex)
            {
                _logger.LogError("扫描{0}身份证件错误：ex:{1} ", _device.ReaderType.Value, ex);
                person = null;
            }
            //LogDebug("结束{0}阅读器！", _device.ReaderType.Value);
            if (person == null)
            {
                return null;
            }
            _logger.LogInformation(JsonConvert.SerializeObject($"person:{person}"));
            //LogDebug("阅读器{0}获取结果：Run=ExecuteEveryRead {1} ", _device.ReaderType.Value, JsonConvert.SerializeObject(person));
            return person;
        }
        /// <summary>
        /// 烧苗证件  手动点击按钮
        /// </summary>
        /// <param name="operateIdType"></param>
        /// <returns></returns>
        public async Task<Person> ExecuteEveryScanAsync(string operateIdType)
        {
            return await Task.Run(() =>
            {
                Person person = null;
                try
                {
                    //LogDebug("开始{0}烧苗器连接！", _scanDevice.ReaderType.Value);
                    //开启连接
                    _scanDevice.InitComm();
                    try
                    {
                        //LogDebug("开始{0}烧苗器认证！", _scanDevice.ReaderType.Value);
                        //认证
                        _scanDevice.Authenticate();
                        //LogDebug("开始{0}烧苗器读取内容！", _scanDevice.ReaderType.Value);
                        //读取内容    烧苗完成OCR获取类容 2021-08-18
                        person = _scanDevice.ScanContent(operateIdType);
                    }
                    catch (IdReaderException ex)
                    {
                        LogDebug(ex.Message);
                        person = null;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("扫描{0}身份证件错误：ex:{1} ", _scanDevice.ReaderType.Value, ex);
                        person = null;
                    }
                    finally
                    {
                        //LogDebug("开始{0}阅读器关闭连接！", _scanDevice.ReaderType.Value);
                        //关闭连接
                        _scanDevice.CloseComm();
                    }
                }
                catch (IdReaderException ex)
                {
                    LogDebug(ex.Message);
                    person = null;
                }
                catch (Exception ex)
                {
                    _logger.LogError("扫描{0}身份证件错误：ex:{1} ", _scanDevice.ReaderType.Value, ex);
                    person = null;
                }
                //LogDebug("结束{0}阅读器！", _scanDevice.ReaderType.Value);
                if (person == null)
                {
                    return null;
                }
                //LogDebug("阅读器{0}获取结果：Run=ExecuteEveryScanAsync {1} ", _scanDevice.ReaderType.Value, JsonConvert.SerializeObject(person));
                return person;
            });
        }
    }
}
