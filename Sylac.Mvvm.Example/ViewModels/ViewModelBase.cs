using ReactiveUI;
using Sylac.Mvvm.Abstraction;

namespace Sylac.Mvvm.Example.ViewModels;

public abstract class ViewModelBase<TViewModelParameters> : ReactiveObject, IViewModel<TViewModelParameters>
    where TViewModelParameters : IViewModelParameters
{
    /// <inheritdoc cref="IViewModel{TParam}.Initialize"/>
    public void Initialize(IViewModelParameters parameter)
    {
        if (parameter is TViewModelParameters viewModelParameters)
        {
            OnLoadedParameters(viewModelParameters);
        }
    }

    public virtual void OnLoadedParameters(TViewModelParameters parameters) { }

    /// <inheritdoc cref="IViewModel.OnNavigatedFrom"/>
    public virtual void OnNavigatedFrom() { }

    /// <inheritdoc cref="IViewModel.OnNavigatedTo"/>
    public virtual void OnNavigatedTo() { }

    /// <inheritdoc cref="IViewModel.OnNavigatingFrom"/>
    public virtual void OnNavigatingFrom() { }

    /// <inheritdoc cref="IViewModel.OnNavigatingTo"/>
    public virtual void OnNavigatingTo() { }
}
