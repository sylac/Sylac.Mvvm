using Sylac.Mvvm.Example.ViewModels;
using Sylac.Mvvm.Maui.Controls;
using Sylac.Mvvm.Navigation;

namespace Sylac.Mvvm.Example;

[ConnectWithViewModel<ExamplePageViewModel, ExamplePageViewModelParameters>]
public partial class ExamplePage : NavigationablePage
{
    public ExamplePage(ExamplePageViewModel examplePageViewModel)
        : base(examplePageViewModel)
    {
        InitializeComponent();
    }
}