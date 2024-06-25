namespace Sylac.Mvvm;

public interface IViewModel
{
    virtual void OnAppearing() { }
}

public interface IViewModel<out TParam> : IViewModel
    where TParam : IViewModelParameters
{
    void Initialize(IViewModelParameters parameter);
}
