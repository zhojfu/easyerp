namespace EasyERP.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Organize
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string OrgnizeId { get; set; }

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

        public bool IsReady { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        public string SocialName { get; set; }

        #region Foreign Keys

        public string OrgnizeTypeId { get; set; }

        public OrganizeType OrgnizeType { get; set; }

        public string CurrencyId { get; set; }

        public Currency Currency { get; set; }

        #endregion Foreign Keys
    }
}