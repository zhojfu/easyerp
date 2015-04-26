namespace EasyErp.Core
{
    using Domain.Model.Users;

    public interface IWorkContext
    {
        User CurrentUser { get; set; }
    }
}