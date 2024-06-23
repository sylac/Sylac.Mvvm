namespace Sylac.MVVM;

public interface IViewModel
{
    virtual void OnAppearing() { }
}

public interface IViewModel<TParam> : IViewModel
    where TParam : IViewModelParameters
{
    virtual void Initialize(TParam parameter) { }
}
