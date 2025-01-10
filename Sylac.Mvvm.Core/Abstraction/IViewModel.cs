namespace Sylac.Mvvm.Abstraction;

public interface IViewModel
{
    /// <summary>
    /// Called when the navigation to the view model is initiated.
    /// </summary>
    void OnNavigatingTo();

    /// <summary>
    /// Called when the navigation from the view model is initiated.
    /// </summary>
    void OnNavigatingFrom();

    /// <summary>
    /// Called when the navigation to the view model is completed.
    /// </summary>
    void OnNavigatedTo();

    /// <summary>
    /// Called when the navigation from the view model is completed.
    /// </summary>
    void OnNavigatedFrom();
}

public interface IViewModel<out TParam> : IViewModel
    where TParam : IViewModelParameters
{
    /// <summary>
    /// Initializes the view model with the provided parameters.
    /// </summary>
    /// <param name="parameter"> The parameters to initialize the view model with. </param>
    void Initialize(IViewModelParameters parameter);
}
