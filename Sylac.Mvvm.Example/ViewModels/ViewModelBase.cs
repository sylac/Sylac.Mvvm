using ReactiveUI;

namespace Sylac.Mvvm.Example.ViewModels;

public abstract class ViewModelBase<TViewModelParameters> : ReactiveObject, IViewModel<TViewModelParameters>
    where TViewModelParameters : IViewModelParameters
{
    public void Initialize(IViewModelParameters parameter)
    {
        if (parameter is TViewModelParameters viewModelParameters)
        {
            OnLoadedParameters(viewModelParameters);
        }
    }

    public virtual void OnLoadedParameters(TViewModelParameters parameters) { }

    /// <summary>
    /// Called when the navigation from the view model is completed.
    /// </summary>
    public virtual void OnNavigatedFrom() { }

    /// <summary>
    /// Called when the navigation to the view model is completed.
    /// </summary>
    public virtual void OnNavigatedTo() { }

    /// <summary>
    /// Called when the navigation from the view model is initiated.
    /// </summary>
    public virtual void OnNavigatingFrom() { }

    /// <summary>
    /// Called when the navigation to the view model is initiated.
    /// </summary>
    public virtual void OnNavigatingTo() { }
}
