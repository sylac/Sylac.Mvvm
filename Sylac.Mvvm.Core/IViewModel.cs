namespace Sylac.Mvvm;

public interface IViewModel
{
    void OnNavigatingTo();
    void OnNavigatingFrom() { }
    void OnNavigatedTo();
    void OnNavigatedFrom();
}

public interface IViewModel<out TParam> : IViewModel
    where TParam : IViewModelParameters
{
    void Initialize(IViewModelParameters parameter);
}
