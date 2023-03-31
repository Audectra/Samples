using System;
using Microsoft.Extensions.Logging;
using SampleApp.Common.Utilities;

namespace SampleApp;

internal class RxAppExceptionHandler : IObserver<Exception>
{
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
        var logger = LogManager.GetLogger<RxAppExceptionHandler>();
        logger.LogCritical(error, "RxApp Exception Handler Exception");
    }

    public void OnNext(Exception value)
    {
        var logger = LogManager.GetLogger<RxAppExceptionHandler>();
        logger.LogCritical(value, "Unhandled RxApp Exception");
    }
}
