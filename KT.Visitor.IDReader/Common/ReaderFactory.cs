using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Visitor.IdReader.Common
{
    /// <summary>
    /// 初始化阅读器工厂,静态
    /// </summary>
    public class ReaderFactory
    {
        //全局Reader
        public static IReader Reader { get; set; }

        private ILogger<ReaderFactory> _logger;
        private IContainerProvider _serviceProvider;

        //证件阅读器定时器,用于定时读取身份证件信息
        private Timer readerTimer;
        private Func<Person, Task> _setPersonAction;

        private IReader _reader;
        private IdReaderSettings _idReaderSettings;

        private readonly object _locker = new object();

        public ReaderFactory(ILogger<ReaderFactory> logger,
            IContainerProvider serviceProvider,
            IReader reader,
            IdReaderSettings idReaderSettings)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _reader = reader;
            _idReaderSettings = idReaderSettings;
        }

        public async Task<IReader> StartAsync(string name, Action<Person> setPerson, bool isEveryConnect)
        {
            var setPersonAsync = new Func<Person, Task>(async (person) =>
            {
                await Task.Run(() =>
                {
                    setPerson.Invoke(person);
                });
            });
            return await StartAsync(name, setPersonAsync, isEveryConnect);
        }

        public async Task<IReader> StartAsync(string name, Func<Person, Task> setPersonAsync, bool isEveryConnect)
        {
            _setPersonAction = setPersonAsync;

            _logger.LogInformation("证件阅读器正在开启：oldReader:{0} newReader ", _reader?.GetReaderType(), name);

            //先关闭现有证件阅读器连接
            if (_reader != null && !string.IsNullOrEmpty(_reader?.GetReaderType()))
            {
                _logger.LogInformation("证件阅读器已开启：{0} ", name);
                _reader.Close();
            }

            _logger.LogDebug("读取证件阅读器：{0} ", name);
            if (name == ReaderTypeEnum.HD900.Value)
            {
                var sdk = _serviceProvider.Resolve<HD900>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.HD900.Value);
            }
            else if (name == ReaderTypeEnum.CVR100U.Value)
            {
                var sdk = _serviceProvider.Resolve<CVR100U>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.CVR100U.Value);
            }
            else if (name == ReaderTypeEnum.CVR100XG.Value)
            {
                var sdk = _serviceProvider.Resolve<CVR100XG>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.CVR100XG.Value);
            }
            else if (name == ReaderTypeEnum.THPR210.Value)
            {
                //var sdk = _serviceProvider.Resolve<THPR210>();
                //_reader.Init(sdk, null, false, isEveryConnect);
                var sdk = _serviceProvider.Resolve<THPR210X64>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.THPR210.Value);
            }
            else if (name == ReaderTypeEnum.IDR210.Value)
            {
                var sdk = _serviceProvider.Resolve<IDR210>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.IDR210.Value);
            }
            else if (name == ReaderTypeEnum.FS531.Value)
            {
                var sdk = _serviceProvider.Resolve<FS531>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.FS531.Value);
            }
            else if (name == ReaderTypeEnum.IDR210_FS531.Value)
            {
                var sdk = _serviceProvider.Resolve<IDR210>();
                var scanSdk = _serviceProvider.Resolve<FS531>();
                _reader.Init(sdk, scanSdk, false, isEveryConnect, ReaderTypeEnum.IDR210_FS531.Value);
            }
            else if (name == ReaderTypeEnum.IDR210_FS531X.Value)//2022-02-13
            {
                var sdk = _serviceProvider.Resolve<IDR210X>();
                var scanSdk = _serviceProvider.Resolve<FS531X>();
                _reader.Init(sdk, scanSdk, false, isEveryConnect, ReaderTypeEnum.IDR210_FS531X.Value);
            }
            else if (name == ReaderTypeEnum.THPR210_CVR100U.Value)
            {
                var sdk = _serviceProvider.Resolve<CVR100U>();
                //var scanSdk = _serviceProvider.Resolve<THPR210>();
                var scanSdk = _serviceProvider.Resolve<THPR210X64>();
                _reader.Init(sdk, scanSdk, true, isEveryConnect, ReaderTypeEnum.THPR210_CVR100U.Value);
            }
            else if (name == ReaderTypeEnum.DSK5022.Value)
            {
                var sdk = _serviceProvider.Resolve<DSK5022>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.DSK5022.Value);
            }
            else if (name == ReaderTypeEnum.DEVELOPMENT.Value)
            {
                var sdk = _serviceProvider.Resolve<Development>();
                _reader.Init(sdk, null, false, isEveryConnect, ReaderTypeEnum.DEVELOPMENT.Value);
            }
            else
            {
                throw IdReaderException.Run($"找不到阅读设备：{name} ");
            }
            /*if (name == ReaderTypeEnum.THPR210.Value)
            {
                return _reader;
            }*/
            lock (_locker)
            {
                //阅读器读取身份证
                if (readerTimer != null)
                {
                    readerTimer.Dispose();
                }
                //定时读身份证
                readerTimer = new Timer(ReadIdCardCallBack, null, _idReaderSettings.ReadeTimeSecond * 1000, _idReaderSettings.ReadeTimeSecond * 1000);
            }

            return _reader;
        }

        /// <summary>
        /// 读取身份证信息事件
        /// </summary>
        private async void ReadIdCardCallBack(object state)
        {
            lock (_locker)
            {
                readerTimer?.Change(-1, _idReaderSettings.ReadeTimeSecond * 1000);
            }
            try
            {
                Person person = await _reader?.ReadAsync();
                //_logger.LogInformation($"ReadIdCardCallBack={JsonConvert.SerializeObject(person)}");
                if (person != null)
                {
                    _setPersonAction?.Invoke(person);
                    /*.ContinueWith((task) =>
                    {
                        lock (_locker)
                        {
                            readerTimer?.Change(_idReaderSettings.ReadeTimeSecond * 1000, _idReaderSettings.ReadeTimeSecond * 1000);
                        }
                    });*/
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"证件扫描完成出错：{ex} ");
                throw ex;
            }
            finally
            {
                lock (_locker)
                {
                    readerTimer?.Change(_idReaderSettings.ReadeTimeSecond * 1000, _idReaderSettings.ReadeTimeSecond * 1000);
                }
            }
        }

        public Task DisposeReaderAsync()
        {
            lock (_locker)
            {
                //异步销毁，防止销毁了还操作
                if (readerTimer != null)
                {
                    readerTimer.Dispose();
                    readerTimer = null;
                }
            }
            if (_reader != null)
            {
                _reader?.Close();
            }

            return Task.CompletedTask;
        }
    }
}
