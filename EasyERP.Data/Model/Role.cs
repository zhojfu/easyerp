namespace EasyERP.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Role
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string RoleId { get; set; }

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
        [MaxLength(60)]
        [Required]
        public string UserLevel { get; set; }

        public bool IsManual { get; set; }

        public bool IsAdvanced { get; set; }

        public bool IsRestrictBacked { get; set; }

        public bool IsPortal { get; set; }

        public bool IsPortalAdmin { get; set; }

        #region Foreign Keys

        public string CurrencyId { get; set; }

        public Currency Currency { get; set; }

        #endregion Foreign Keys
    }
}