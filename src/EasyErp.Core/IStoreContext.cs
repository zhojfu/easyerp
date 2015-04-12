namespace EasyErp.Core
{
    /// <summary>
    /// Store context
    /// </summary>
    public interface IStoreContext
    {
        Store CurrentStore { get; }
    }
}