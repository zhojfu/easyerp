namespace Infrastructure.Desktop
{
    public interface IStartupTask
    {
        int Order { get; }

        void Execute();
    }
}