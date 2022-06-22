using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KT.Front.WriteCard.Util
{
    public class LogHelper<T> : ILogger<T>
    {
        private ILog _log;
        public ILog Logger {
            get {
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

    public class CreateRepositoryHelper
    {
        private static object locker = new object();
        private static ILoggerRepository _repository;
        public static ILoggerRepository Repository {
            get {
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
}
