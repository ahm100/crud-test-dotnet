namespace Vehicle.Shared.Resources
{
    public interface ISharedLocalizer<T>
    {
        string this[string key] { get; }
    }
}
