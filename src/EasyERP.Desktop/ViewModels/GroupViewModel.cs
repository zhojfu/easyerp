namespace EasyERP.Desktop.ViewModels
{
    using PropertyChanged;
    using System.Collections.ObjectModel;
    using Telerik.Windows.Controls;

    [ImplementPropertyChanged]
    public class GroupViewModel : ViewModelBase
    {
        public GroupViewModel()
        {
            this.Buttons = new ObservableCollection<BaseButtonViewModel>();
            this.VariantsSource = new ObservableCollection<GroupVariant>();
            this.VariantsSource.Add(new GroupVariant(RibbonGroupVariant.Collapsed, 0));
            this.VariantsSource.Add(new GroupVariant(RibbonGroupVariant.Small, 0));
            this.VariantsSource.Add(new GroupVariant(RibbonGroupVariant.Medium, 0));
            this.VariantsSource.Add(new GroupVariant(RibbonGroupVariant.Large, 0));
        }

        public ObservableCollection<GroupVariant> VariantsSource { get; internal set; }

        public string GroupHeaderText { get; set; }

        [DoNotNotify]
        public ObservableCollection<BaseButtonViewModel> Buttons { get; set; }
    }
}