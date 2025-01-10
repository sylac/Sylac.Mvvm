using Sylac.Mvvm.Abstraction;
using Sylac.Mvvm.Navigation.Abstractions;

namespace Sylac.Mvvm.Maui.Controls;

public partial class NavigablePage : ContentPage, INavigationablePage, IQueryAttributable
{
    public IViewModel<IViewModelParameters> ViewModel { get; private set; }

    public NavigablePage(IViewModel<IViewModelParameters> viewModel)
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

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        ViewModel.OnNavigatedFrom();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ViewModel.OnNavigatedTo();
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        ViewModel.OnNavigatingFrom();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.OnNavigatingTo();
    }
}
