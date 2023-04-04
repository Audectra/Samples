using System;
using System.Reactive.Disposables;
using Microsoft.Extensions.Logging;

namespace SampleApp.Common.Utilities;

public class LogManager
{
    private static readonly Lazy<LogManager> LazyInstance = new(() => new LogManager());
    private static readonly Lazy<NoLogger> LazyNoLogger = new(() => new NoLogger());

    public static LogManager Current => LazyInstance.Value;
    
    public ILoggerFactory? Factory { get; set; }

    private LogManager()
    {
    }

    public static ILogger GetLogger<T>() => (ILogger?)Current.Factory?.CreateLogger<T>() ?? LazyNoLogger.Value;
    public static ILogger GetLogger(string loggerName) => Current.Factory?.CreateLogger(loggerName) ?? LazyNoLogger.Value;

    private class NoLogger : ILogger
    {
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
        public bool IsEnabled(LogLevel logLevel) => false;
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => new CompositeDisposable();
    }
}
