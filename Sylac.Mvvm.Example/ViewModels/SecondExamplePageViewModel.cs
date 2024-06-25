namespace Sylac.Mvvm.Example.ViewModels
{
    /// <summary>
    /// The parameters required to initialize the <see cref="ExamplePageViewModel"/>.
    /// </summary>
    public sealed record SecondExamplePageViewModelParameters() : IViewModelParameters;

    /// <summary>
    /// Implementation of the example page view model.
    /// </summary>
    public sealed class SecondExamplePageViewModel
        : ViewModelBase<SecondExamplePageViewModelParameters>
    {
        public override void OnLoadedParameters(SecondExamplePageViewModelParameters parameters) { }
    }
}
