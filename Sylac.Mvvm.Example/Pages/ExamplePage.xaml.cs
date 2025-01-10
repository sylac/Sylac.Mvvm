using Sylac.Mvvm.Example.ViewModels;
using Sylac.Mvvm.Maui.Controls;
using Sylac.Mvvm.Navigation;

namespace Sylac.Mvvm.Example.Pages;

[ConnectWithViewModel<ExamplePageViewModel, ExamplePageViewModelParameters>]
public partial class ExamplePage : NavigablePage
{
    public ExamplePage(ExamplePageViewModel examplePageViewModel)
        : base(examplePageViewModel)
    {
        InitializeComponent();
    }
}