namespace EasyERP.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(32)]
        public string UserId { get; set; }

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

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        [Required]
        public string Password { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }

        public bool Processing { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        public string EmailUser { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        public string EmailUserPwd { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(2000)]
        public string Comments { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        public string Phone1 { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(40)]
        public string Phone2 { get; set; }

        [Timestamp]
        public byte[] Brithday { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        public bool IsStocked { get; set; }

        [Required]
        public bool GrantPortalAccess { get; set; }

        #region Foreign Keys

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        public string RoleId { get; set; }

        public Role Role { get; set; }

        public string OgnizeId { get; set; }

        [ForeignKey("OgnizeId")]
        public Orgnize Orgnize { get; set; }

        public string DefaultWareHouseId { get; set; }

        [ForeignKey("DefaultWareHouseId")]
        public WareHouse WareHouse { get; set; }

        public string SuperVisorId { get; set; }

        public User SuperVisorUser { get; set; }

        #endregion Foreign Keys
    }
}