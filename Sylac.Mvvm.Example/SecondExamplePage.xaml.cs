using Sylac.Mvvm.Example.ViewModels;
using Sylac.Mvvm.Maui.Controls;

namespace Sylac.Mvvm.Example;

public partial class SecondExamplePage : NavigationablePage
{
    public SecondExamplePage(SecondExamplePageViewModel pageViewModel)
        : base(pageViewModel)
    {
        InitializeComponent();
    }
}