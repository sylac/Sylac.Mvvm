namespace Sylac.MVVM.Navigation
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ConnectWithViewModelAttribute<TViewModel, TParams>() : Attribute
        where TViewModel : IViewModel<TParams>
        where TParams : IViewModelParameters, new()
    {
    }
}
