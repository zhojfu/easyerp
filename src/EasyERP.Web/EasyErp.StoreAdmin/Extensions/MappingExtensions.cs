namespace EasyErp.StoreAdmin.Extensions
{
    using Domain.Model.Products;
    using EasyErp.StoreAdmin.Models.Products;

    public static class MappingExtensions
    {
        public static ProductModel ToModel(this Product entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ProductModel
            {
                Id = entity.Id,
                FullDescription = entity.FullDescription,
                Gtin = entity.Gtin,
                Price = entity.Price,
                Name = entity.Name
            };
        }
    }
}