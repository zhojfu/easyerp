namespace EasyERP.Desktop.ViewModels
{
    using System.Windows;
    using System.Windows.Controls;

    public class ButtonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ButtonTemplate { get; set; }

        public DataTemplate SplitButtonTemplate { get; set; }

        public DataTemplate DropDownSplitButtonTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ButtonViewModel)
            {
                return this.ButtonTemplate;
            }
            if (item is SplitButtonViewModel)
            {
                return this.SplitButtonTemplate;
            }
            if (item is DropDownButtonViewModel)
            {
                return this.DropDownSplitButtonTemplate;
            }

            return null;
        }
    }
}