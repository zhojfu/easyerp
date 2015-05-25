namespace Doamin.Service.Products
{
    using System.Collections.Generic;
    using Domain.Model.Products;
    using EasyErp.Core;

    /// <summary>
    /// Manufacturer service
    /// </summary>
    public interface IManufacturerService
    {
        void DeleteManufacturer(Manufacturer manufacturer);

        IPagedList<Manufacturer> GetAllManufacturers(
            string manufacturerName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false);

        Manufacturer GetManufacturerById(long manufacturerId);
        void InsertManufacturer(Manufacturer manufacturer);
        void UpdateManufacturer(Manufacturer manufacturer);
        void DeleteProductManufacturer(ProductManufacturer productManufacturer);

        IPagedList<ProductManufacturer> GetProductManufacturersByManufacturerId(
            long manufacturerId,
            int pageIndex,
            int pageSize,
            bool showHidden = false);

        IList<ProductManufacturer> GetProductManufacturersByProductId(long productId, bool showHidden = false);
        ProductManufacturer GetProductManufacturerById(long productManufacturerId);
        void InsertProductManufacturer(ProductManufacturer productManufacturer);
        void UpdateProductManufacturer(ProductManufacturer productManufacturer);
    }
}