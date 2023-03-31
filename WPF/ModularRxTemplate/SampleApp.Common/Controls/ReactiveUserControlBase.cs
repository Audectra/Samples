using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using SampleApp.Common.ViewModels;

namespace SampleApp.Common.Controls;

public class ReactiveUserControlBase<TViewModel> : ReactiveUserControl<TViewModel>
    where TViewModel : ReactiveViewModelBase
{
    public ReactiveUserControlBase()
    {
        this.WhenActivated(d =>
        {
            this.WhenAnyValue(v => v.DataContext)
                .Select(x => x as TViewModel)
                .BindTo(this, v => v.ViewModel)
                .DisposeWith(d);
        });
    }
}
