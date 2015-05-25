namespace Domain.Model.Products
{
    using Domain.Model.Media;
    using Infrastructure.Domain.Model;

    public class ProductPicture : BaseEntity
    {
        public int ProductId { get; set; }

        public int PictureId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual Product Product { get; set; }
    }
}