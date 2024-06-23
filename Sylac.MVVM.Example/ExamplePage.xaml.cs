using Sylac.MVVM.Core.Navigation;
using Sylac.MVVM.Example.ViewModels;
using Sylac.MVVM.MAUI.Controls;

namespace Sylac.MVVM.Example;

[ConnectWithViewModel<ExamplePageViewModel, ExamplePageViewModelParameters>]
public partial class ExamplePage : NavigationablePage
{
    public ExamplePage(ExamplePageViewModel examplePageViewModel)
        : base(examplePageViewModel)
    {
        InitializeComponent();
    }
}