namespace Infrastructure.Desktop.AttachProperty
{
    using System.Windows;

    public static class WindowCloser
    {
        public static void SetIsClose(Window target, bool? value)
        {
            target.SetValue(IsCloseProperty, value);
        }

        private static void IsCloseChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null &&
                (bool?)e.NewValue == true)
            {
                window.Close();
            }
        }

        public static readonly DependencyProperty IsCloseProperty =
            DependencyProperty.RegisterAttached(
                "IsClose",
                typeof(bool?),
                typeof(WindowCloser),
                new PropertyMetadata(IsCloseChanged));
    }
}