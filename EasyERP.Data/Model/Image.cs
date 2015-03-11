namespace EasyERP.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string ImageId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Timestamp]
        [Required]
        public byte[] Created { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        public string CreateBy { get; set; }

        [Required]
        public byte[] Updated { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        public string Updatedy { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(120)]
        public string ImageUrl { get; set; }

        [MaxLength]
        public byte[] BinaryData { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [MaxLength]
        public string MimeType { get; set; }
    }
}