namespace EasyErp.Core.Configuration.Settings
{
    public class AreaSettings : ISettings
    {
        public int DefaultGridPageSize
        {
            get { return 15; }
        }

        public string GridPageSizes
        {
            get { return "10, 15, 20, 50, 100"; }
        }

        public bool DisplayProductPictures { get; set; }

        public string RichEditorAdditionalSettings { get; set; }

        public bool RichEditorAllowJavaScript { get; set; }
    }
}