using Sylac.Mvvm.Core.Navigation;
using Sylac.Mvvm.Example.ViewModels;
using Sylac.Mvvm.Maui.Controls;

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