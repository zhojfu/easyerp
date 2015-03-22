namespace EasyERP.Desktop.ViewModels
{
    using PropertyChanged;
    using System;
    using Telerik.Windows.Controls;
    using Telerik.Windows.Controls.RibbonView;

    [ImplementPropertyChanged]
    public class BaseButtonViewModel : ViewModelBase
    {
        public string ImageSource { get; set; }

        public string ImageText { get; set; }

        public string Content { get; set; }

        public ButtonSize Size { get; set; }
    }
}