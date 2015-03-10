namespace EasyERP
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new EasyErpContext())
            {
                db.SaveChanges();
            }
        }
    }
}