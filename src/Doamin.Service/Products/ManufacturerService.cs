namespace Doamin.Service.Products
{
    using Doamin.Service.Events;
    using Domain.Model.Products;
    using Domain.Model.Security;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Manufacturer service
    /// </summary>
    public partial class ManufacturerService : IManufacturerService
    {
        public void DeleteManufacturer(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Manufacturer> GetAllManufacturers(
            string manufacturerName = "",
            int pageIndex = 0,
            int pageSize = Int32.MaxValue,
            bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetManufacturerById(long manufacturerId)
        {
            throw new NotImplementedException();
        }

        public void InsertManufacturer(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductManufacturer(ProductManufacturer productManufacturer)
        {
            throw new NotImplementedException();
        }

        public IPagedList<ProductManufacturer> GetProductManufacturersByManufacturerId(
            long manufacturerId,
            int pageIndex,
            int pageSize,
            bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public IList<ProductManufacturer> GetProductManufacturersByProductId(long productId, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public ProductManufacturer GetProductManufacturerById(long productManufacturerId)
        {
            throw new NotImplementedException();
        }

        public void InsertProductManufacturer(ProductManufacturer productManufacturer)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductManufacturer(ProductManufacturer productManufacturer)
        {
            throw new NotImplementedException();
        }
    }
}