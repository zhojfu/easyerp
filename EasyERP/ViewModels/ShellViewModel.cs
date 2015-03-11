namespace EasyERP.ViewModels
{
    using Caliburn.Micro;
    using EasyERP.Contracts;
    using System.ComponentModel.Composition;

    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        public ShellViewModel()
        {
            this.DisplayName = "EasyERP";
        }
    }
}