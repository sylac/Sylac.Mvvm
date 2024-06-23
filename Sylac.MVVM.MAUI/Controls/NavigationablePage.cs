using Sylac.MVVM.Navigation.Abstractions;

namespace Sylac.MVVM.MAUI.Controls;

public class NavigationablePage : ContentPage, INavigationablePage, IQueryAttributable
{
    public IViewModel ViewModel { get; }

    public NavigationablePage(IViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
        Title = string.Empty;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(INavigationablePage.ParametersKey, out var parameter) &&
            ViewModel is IViewModel<IViewModelParameters> viewModelWithParams &&
            parameter is IViewModelParameters viewModelParameter)
        {
            viewModelWithParams.Initialize(viewModelParameter);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.OnAppearing();
    }
}
