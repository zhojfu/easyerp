namespace EasyERP.Desktop.ViewModels
{
    using System.Collections.ObjectModel;

    public class TabViewModel
    {
        public TabViewModel()
        {
            this.Groups = new ObservableCollection<GroupViewModel>();
        }

        public string Header { get; set; }

        public string ContextualGroupName { get; set; }

        public ObservableCollection<GroupViewModel> Groups { get; set; }

        public bool IsSelected { get; set; }
    }
}