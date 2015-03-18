namespace EasyERP.Desktop.Contacts
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    public class ViewModelMetadataAttribute : Attribute
    {
        public int Order { get; private set; }

        public ViewModelMetadataAttribute(int order)
        {
            this.Order = order;
        }
    }
}