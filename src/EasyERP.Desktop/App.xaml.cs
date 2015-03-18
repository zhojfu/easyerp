namespace EasyERP.Desktop
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AutofacBootstrapper bootstrapper;

        public App()
        {
            this.InitializeComponent();
            this.bootstrapper = new AutofacBootstrapper();
        }
    }
}