using Microsoft.Extensions.Logging;
using System;

namespace digitalsign.application.Services
{
    public abstract class ServiceBase
    {
        protected ILogger _logger { get; }
        public ServiceBase(ILogger logger)
        {
            _logger = logger;
        }
        protected void Log(LogLevel level, string message, params object[] args)
        {
            _logger.Log(level, message, args);
        }

        protected void Log(LogLevel level, Exception exception, string message, params object[] args)
        {
            _logger.Log(level, exception, message, args);
        }
    }
}
