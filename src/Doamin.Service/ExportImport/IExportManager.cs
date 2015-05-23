namespace Doamin.Service.ExportImport
{
    using System.Collections.Generic;
    using Domain.Model.Products;

    public interface IExportManager
    {
        string ExportProductsToXml(IList<Product> products);
    }
}
