namespace EasyERP.Desktop.ViewModels
{
    using Autofac.Features.Metadata;
    using Caliburn.Micro;
    using EasyERP.Desktop.Contacts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

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

            this.Initialize();
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

        public void SelectionChanged(object sender)
        {
            var model = this.ActiveItem as IViewModel;
            var ribbonTap = sender as FrameworkElement;
            if (ribbonTap == null ||
                model == null)
            {
                return;
            }
            if (model.Tag == ribbonTap.Tag as string)
            {
                return;
            }

            var screen = this.Items.FirstOrDefault(
                i =>
                {
                    var m = i as IViewModel;
                    if (m == null)
                    {
                        return false;
                    }
                    return m.Tag == ribbonTap.Tag as string;
                });
            this.ChangeActiveItem(screen, false);
        }

        private void Initialize()
        {
            this.ActivateItem(this.Items.First());
        }
    }
}