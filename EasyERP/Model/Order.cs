namespace EasyERP.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string OrderId { get; set; }
    }
}