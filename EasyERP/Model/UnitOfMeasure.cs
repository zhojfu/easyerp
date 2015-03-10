namespace EasyERP.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UOM")]
    public class UnitOfMeasure
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string UomId { get; set; }

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

        public string UomSymbol { get; set; }

        [Required]
        public decimal StdPrecision { get; set; }

        [Required]
        public decimal CostingPrecision { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        public string UomType { get; set; }
    }
}