using System;
using SampleApp.Common.ViewModels;

namespace SampleApp.ViewModels;

public class MainWindowViewModel : ReactiveViewModelBase
{
    private string? _username;

    public string? Username
    {
        get => _username;
        set => RaiseAndSetIfChanged(ref _username, value);
    }

    public MainWindowViewModel()
    {
        Username = Environment.UserName;
    }
}
