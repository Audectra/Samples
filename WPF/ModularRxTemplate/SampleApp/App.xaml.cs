using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Modularity;
using ReactiveUI;
using SampleApp.Common.Utilities;
using SampleApp.Modules;
using SampleApp.ViewModels;

namespace SampleApp
{
    public partial class App
    {
        private ILogger _logger = null!;
        
        public App()
        {
            InitLogging();
            InitGlobalExceptionHandlers();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            _logger.LogInformation($"Staring application (v{assemblyVersion}).");
            
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IModuleCatalog, AppModuleCatalog>();
            containerRegistry.RegisterSingleton<MainWindowViewModel>();
        }

        protected override Window CreateShell() => Container.Resolve<MainWindowView>();

        protected override void Initialize()
        {
            base.Initialize();
            
            // We initialize the view model for the main window at this point, which allows it to take
            // any dependencies from other modules in as well. 
            MainWindow!.DataContext = Container.Resolve<MainWindowViewModel>();
        }

        protected override void InitializeModules()
        {
            _logger.LogInformation("Initializing all modules...");
            base.InitializeModules();
            _logger.LogInformation("Modules have been initialized.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _logger.LogInformation("Closing application.");
            base.OnExit(e);
        }

        private void InitLogging()
        {
            // Customize logging here.
            LogManager.Current.Factory = LoggerFactory.Create(b => b
                .ClearProviders()
                .SetMinimumLevel(LogLevel.Trace)
                .AddDebug());

            _logger = LogManager.GetLogger<App>();
        }
        
        #region Global Exception Handlers

    private void InitGlobalExceptionHandlers()
    {
        // Obviously you should never actually handle exceptions in these handlers, but it would be nice to log them.
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_OnUnhandledException;
        Current.DispatcherUnhandledException += Current_OnDispatcherUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskScheduler_OnUnobservedTaskException;

        // ReactiveUI
        RxApp.DefaultExceptionHandler = new RxAppExceptionHandler();
    }

    private void TaskScheduler_OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e) => 
        _logger.LogCritical(e.Exception, "Unhandled TaskScheduler Exception");

    private void Current_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) => 
        _logger.LogCritical(e.Exception, "Unhandled Dispatcher Exception");

    private void CurrentDomain_OnUnhandledException(object sender, UnhandledExceptionEventArgs e) => 
        _logger.LogCritical((Exception)e.ExceptionObject, "Unhandled AppDomain Exception");

    #endregion
    }
}
