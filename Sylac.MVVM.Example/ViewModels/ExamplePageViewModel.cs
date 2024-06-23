using ReactiveUI.Fody.Helpers;
using Sylac.Mvvm.Core;

namespace Sylac.Mvvm.Example.ViewModels
{
    /// <summary>
    /// The parameters required to initialize the <see cref="ExamplePageViewModel"/>.
    /// </summary>
    public sealed record ExamplePageViewModelParameters(string InitialText) : IViewModelParameters;

    /// <summary>
    /// Implementation of the example page view model.
    /// </summary>
    public sealed class ExamplePageViewModel : ViewModelBase<ExamplePageViewModelParameters>
    {
        [Reactive]
        public string EnteredText { get; set; } = "";

        public override void OnLoadedParameters(ExamplePageViewModelParameters parameters)
        {
            EnteredText = parameters.InitialText;
        }
    }
}
