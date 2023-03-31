using System;
using Microsoft.Extensions.Logging;

namespace SampleApp.Common.Utilities;

public class LogManager
{
    private const string LoggerFactoryNotProvided = "Logger factory has not been providede yet.";
    private static readonly Lazy<LogManager> LazyInstance = new(() => new LogManager());

    public static LogManager Current => LazyInstance.Value;
    
    public ILoggerFactory? Factory { get; set; }

    private LogManager()
    {
    }

    public static ILogger GetLogger<T>() => Current.Factory?.CreateLogger<T>() ?? throw new InvalidOperationException(LoggerFactoryNotProvided);
    public static ILogger GetLogger(string loggerName) => Current.Factory?.CreateLogger(loggerName) ?? throw new InvalidOperationException(LoggerFactoryNotProvided);
}
