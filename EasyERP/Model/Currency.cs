namespace EasyERP.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Currency
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string CurrencyId { get; set; }

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

        [Column(TypeName = "char")]
        [StringLength(3)]
        [Required]
        public string IsoCode { get; set; }

        [Column(TypeName = "ntext")]
        public string CurrencySymbol { get; set; }

        [Required]
        public int StdPrecision { get; set; }

        [Required]
        public int CostingPrecision { get; set; }

        [Required]
        public int PricePrecision { get; set; }
    }
}