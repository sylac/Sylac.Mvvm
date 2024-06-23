using Sylac.Mvvm.Core;
using Sylac.Mvvm.Core.Navigation.Abstractions;

namespace Sylac.Mvvm.Maui.Controls;

public class NavigationablePage : ContentPage, INavigationablePage, IQueryAttributable
{
    public IViewModel<IViewModelParameters> ViewModel { get; private set; }

    public NavigationablePage(IViewModel<IViewModelParameters> viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
        Title = string.Empty;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(INavigationablePage.ParametersKey, out var parameter) &&
            parameter is IViewModelParameters viewModelParameter)
        {
            ViewModel.Initialize(viewModelParameter);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.OnAppearing();
    }
}
