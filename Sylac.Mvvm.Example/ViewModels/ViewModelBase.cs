using ReactiveUI;

namespace Sylac.Mvvm.Example.ViewModels
{
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

        public abstract void OnLoadedParameters(TViewModelParameters parameters);
    }
}
