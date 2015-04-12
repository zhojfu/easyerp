namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;
    using System;
    using System.Collections.Generic;

    public class SpecificationAttributeService : ISpecificationAttributeService
    {
        public SpecificationAttribute GetSpecificationAttributeById(int specificationAttributeId)
        {
            throw new NotImplementedException();
        }

        public IPagedList<SpecificationAttribute> GetSpecificationAttributes(int pageIndex = 0, int pageSize = Int32.MaxValue)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecificationAttribute(SpecificationAttribute specificationAttribute)
        {
            throw new NotImplementedException();
        }

        public void InsertSpecificationAttribute(SpecificationAttribute specificationAttribute)
        {
            throw new NotImplementedException();
        }

        public void UpdateSpecificationAttribute(SpecificationAttribute specificationAttribute)
        {
            throw new NotImplementedException();
        }

        public SpecificationAttributeOption GetSpecificationAttributeOptionById(int specificationAttributeOption)
        {
            throw new NotImplementedException();
        }

        public IList<SpecificationAttributeOption> GetSpecificationAttributeOptionsByIds(int[] specificationAttributeOptionIds)
        {
            throw new NotImplementedException();
        }

        public IList<SpecificationAttributeOption> GetSpecificationAttributeOptionsBySpecificationAttribute(int specificationAttributeId)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecificationAttributeOption(SpecificationAttributeOption specificationAttributeOption)
        {
            throw new NotImplementedException();
        }

        public void InsertSpecificationAttributeOption(SpecificationAttributeOption specificationAttributeOption)
        {
            throw new NotImplementedException();
        }

        public void UpdateSpecificationAttributeOption(SpecificationAttributeOption specificationAttributeOption)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductSpecificationAttribute(ProductSpecificationAttribute productSpecificationAttribute)
        {
            throw new NotImplementedException();
        }

        public IList<ProductSpecificationAttribute> GetProductSpecificationAttributesByProductId(int productId)
        {
            throw new NotImplementedException();
        }

        public IList<ProductSpecificationAttribute> GetProductSpecificationAttributesByProductId(int productId, bool? allowFiltering, bool? showOnProductPage)
        {
            throw new NotImplementedException();
        }

        public ProductSpecificationAttribute GetProductSpecificationAttributeById(int productSpecificationAttributeId)
        {
            throw new NotImplementedException();
        }

        public void InsertProductSpecificationAttribute(ProductSpecificationAttribute productSpecificationAttribute)
        {
            throw new NotImplementedException();
        }

        public void UpdateProductSpecificationAttribute(ProductSpecificationAttribute productSpecificationAttribute)
        {
            throw new NotImplementedException();
        }
    }
}