using Sylac.Mvvm.Abstraction;

namespace Sylac.Mvvm.Example.ViewModels;

public record ShellNavigationPageViewModelParameters : IViewModelParameters;
public class ShellTabPageViewModel : ViewModelBase<ShellNavigationPageViewModelParameters>
{
}
