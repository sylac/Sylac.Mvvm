namespace Sylac.Mvvm.Example.ViewModels;

public record ShellNavigationPageViewModelParameters : IViewModelParameters;
public class ShellTabPageViewModel() : ViewModelBase<ShellNavigationPageViewModelParameters>
{

    public override void OnNavigatedTo()
    {
        base.OnNavigatedTo();
    }
}
