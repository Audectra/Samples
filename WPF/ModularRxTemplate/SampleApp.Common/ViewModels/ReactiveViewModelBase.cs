using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace SampleApp.Common.ViewModels;

public class ReactiveViewModelBase : ReactiveObject
{
    protected bool RaiseAndSetIfChanged<TValue>(ref TValue backingField, TValue value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<TValue>.Default.Equals(backingField, value))
            return false;

        backingField = value;
        RaisePropertyChanged(propertyName);

        return true;
    }
    
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) =>
        IReactiveObjectExtensions.RaisePropertyChanged(this, propertyName);

    /* Some UI frameworks for WPF require the property changes to be propagated via the main UI thread.
     * If your UI framework has such a requirement, then simply use this method instead of the above one. 
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null) =>
        RxApp.MainThreadScheduler.Schedule(() => IReactiveObjectExtensions.RaisePropertyChanged(this, propertyName));
    */
}
