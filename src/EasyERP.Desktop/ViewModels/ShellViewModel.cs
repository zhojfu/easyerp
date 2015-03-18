namespace EasyERP.Desktop.ViewModels
{
    using Autofac.Features.Metadata;
    using Caliburn.Micro;
    using EasyERP.Desktop.Contacts;
    using System.Collections.Generic;
    using System.Linq;

    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IShell
    {
        public ShellViewModel(IEnumerable<Meta<IViewModel>> viewModels)
        {
            this.Items.AddRange(viewModels.OrderBy(m => m.Metadata["Order"]).Select(a => a.Value).OfType<Screen>());
            this.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "ActiveItem")
                {
                    this.NotifyOfPropertyChange(() => this.CanCloseActiveItem);
                }
            };
        }

        public override string DisplayName
        {
            get { return "Login Page"; }
            set { }
        }

        public bool CanCloseActiveItem
        {
            get { return this.ActiveItem != null; }
        }

        public void CloseActiveItem()
        {
            this.DeactivateItem(this.ActiveItem, true);
        }
    }
}