namespace EasyERP.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Locator
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string CategoryId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Timestamp]
        [Required]
        public byte[] Created { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required]
        public string CreateBy { get; set; }

        [Timestamp]
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

        public bool IsDefault { get; set; }

        [Required]
        public int PriorityNo { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        public string X { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        public string Z { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        public string Y { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string BarCode { get; set; }

        #region Foreign Keys

        public string WareHouseId { get; set; }

        [ForeignKey("WareHouseId")]
        public WareHouse WareHouse { get; set; }

        #endregion Foreign Keys
    }
}