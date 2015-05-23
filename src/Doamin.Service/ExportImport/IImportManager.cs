namespace Doamin.Service.ExportImport
{
    using System.IO;

    public interface IImportManager
    {
        void ImportProductsFromXlsx(Stream stream);
    }
}