using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KT.Common.WpfApp.Helpers
{

    //    public class CommonLogger : ILoggerFacade
    //    {
    //        private readonly ILog _log = null;

    //        public CommonLogger()
    //        {
    //            if (_log == null)
    //            {
    //                ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
    //                XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
    //                _log = LogManager.GetLogger(repository.Name, "NETCorelog4net");
    //            }
    //        }

    //        #region ILoggerFacade 成员

    //        public void Log(string message, Category category, Priority priority)
    //        {
    //            if (string.IsNullOrEmpty(message))
    //            {
    //                return;
    //            }

    //            switch (category)
    //            {
    //                case Category.Debug:
    //                    this.Debug(message);
    //                    break;
    //                case Category.Exception:
    //                    this.Error(message);
    //                    break;
    //                case Category.Info:
    //                    this.Info(message);
    //                    break;
    //                case Category.Warn:
    //                    this.Fatal(message);
    //                    break;
    //            }
    //        }

    //        #endregion


    //        void Debug(string message)
    //        {
    //            _log.Debug(message);
    //        }

    //        void Info(string message)
    //        {
    //            _log.Info(message);
    //        }

    //        void Error(string message)
    //        {
    //            _log.Error(message);
    //        }

    //        void Fatal(string message)
    //        {
    //            _log.Fatal(message);
    //        }
    //    }
    //}


    public class Log4gProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new Log4gHelper(categoryName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class Log4gHelper : ILogger
    {
        public Log4gHelper()
        {

        }
        private string _categoryName = "DefaultLog4net";
        public Log4gHelper(string categoryName)
        {
            _categoryName = categoryName;
        }
        private ILog _log;
        public ILog Logger
        {
            get
            {
                if (_log == null)
                {
                    _log = GetLog();
                }
                return _log;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public ILog GetLog()
        {
            ILoggerRepository repository = CreateRepositoryHelper.Repository;
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            var log = LogManager.GetLogger(repository.Name, _categoryName);
            return log;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter?.Invoke(state, exception);
            switch (logLevel)
            {
                case LogLevel.Debug:
                    this.Debug(message);
                    break;
                case LogLevel.Error:
                    this.Error(message);
                    break;
                case LogLevel.Information:
                    this.Info(message);
                    break;
                case LogLevel.Warning:
                    this.Fatal(message);
                    break;
            }
        }

        private void Debug(string message)
        {
            Logger.Debug(message);
        }

        private void Info(string message)
        {
            Logger.Info(message);
        }

        private void Error(string message)
        {
            Logger.Error(message);
        }

        private void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public static Log4gHelper<T> CreateType<T>(T logObj)
        {
            return new Log4gHelper<T>();
        }
    }

    public class CreateRepositoryHelper
    {
        private static object locker = new object();
        private static ILoggerRepository _repository;
        public static ILoggerRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    lock (locker)
                    {
                        if (_repository == null)
                        {
                            _repository = LogManager.CreateRepository("WpfAppLoggerRepository");
                        }
                    }

                }
                return _repository;
            }
        }
    }


    public class Log4gHelper<T> : ILogger<T>
    {
        private ILog _log;
        public ILog Logger
        {
            get
            {
                if (_log == null)
                {
                    _log = GetLog();
                }
                return _log;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public ILog GetLog()
        {
            ILoggerRepository repository = CreateRepositoryHelper.Repository;
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            var log = LogManager.GetLogger(repository.Name, typeof(T).Name);
            return log;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter?.Invoke(state, exception);
            switch (logLevel)
            {
                case LogLevel.Debug:
                    this.Debug(message);
                    break;
                case LogLevel.Error:
                    this.Error(message);
                    break;
                case LogLevel.Information:
                    this.Info(message);
                    break;
                case LogLevel.Warning:
                    this.Fatal(message);
                    break;
            }
        }

        private void Debug(string message)
        {
            Logger.Debug(message);
        }

        private void Info(string message)
        {
            Logger.Info(message);
        }

        private void Error(string message)
        {
            Logger.Error(message);
        }

        private void Fatal(string message)
        {
            Logger.Fatal(message);
        }
    }
}


//public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
//{
//    return factory.AddLog4Net(new Log4NetProviderOptions());
//}

//public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, String log4NetConfigFile)
//{
//    //IL_0001: Unknown result type (might be due to invalid IL or missing references)
//    return factory.AddLog4Net(log4NetConfigFile, false);
//}

//public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, String log4NetConfigFile, Boolean watch)
//{
//    //IL_0001: Unknown result type (might be due to invalid IL or missing references)
//    //IL_0002: Unknown result type (might be due to invalid IL or missing references)
//    return factory.AddLog4Net(new Log4NetProviderOptions(log4NetConfigFile, watch));
//}

//public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, Log4NetProviderOptions options)
//{
//    //IL_0007: Unknown result type (might be due to invalid IL or missing references)
//    factory.AddProvider(new Log4NetProvider(options));
//    return factory;
//}

//public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder)
//{
//    Log4NetProviderOptions options = new Log4NetProviderOptions();
//    return builder.AddLog4Net(options);
//}

//public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, String log4NetConfigFile)
//{
//    //IL_0000: Unknown result type (might be due to invalid IL or missing references)
//    Log4NetProviderOptions options = new Log4NetProviderOptions(log4NetConfigFile);
//    return builder.AddLog4Net(options);
//}

//public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, String log4NetConfigFile, Boolean watch)
//{
//    //IL_0000: Unknown result type (might be due to invalid IL or missing references)
//    //IL_0001: Unknown result type (might be due to invalid IL or missing references)
//    Log4NetProviderOptions options = new Log4NetProviderOptions(log4NetConfigFile, watch);
//    return builder.AddLog4Net(options);
//}

////public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, Log4NetProviderOptions options)
////{
////    ServiceCollectionServiceExtensions.AddSingleton<ILoggerProvider>(builder.get_Services(), new Log4NetProvider(options));
////    return builder;
////}

