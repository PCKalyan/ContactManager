using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    [Table("AddressBook")]
    public partial class AddressBook
    {
        [Key]
        [Column("PKAddressId")]
        public int PkaddressId { get; set; }
        [Column("FKStateId")]
        [Display(Name = "State")]
        public int FkstateId { get; set; }
        [Column("FKUserId")]
        [Display(Name = "UserName")]
        public int FkuserId { get; set; }
        [Required]
        [RegularExpression("^([A-Z][a-z]+ ?[A-Z][a-z]+|[A-Z][a-z]+)$", ErrorMessage = "Frist latter should be capital for every word")]
        public string FirstName { get; set; }
        [RegularExpression("^([A-Z][a-z]+ ?[A-Z][a-z]+|[A-Z][a-z]+)$", ErrorMessage = "Frist latter should be capital for every word")]
        public string? LastName { get; set; }
        [StringLength(50)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        [RegularExpression("^\\d{10}$",ErrorMessage = "1. It is vaild only Numbers \r\n 2.Phone number length should be 10")]
        public string PhoneNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [RegularExpression("^[A-Z][a-z]+$", ErrorMessage = "Frist latter should be capital for every word")]
        [Display(Name = "Present Address")]
        public string? Address1 { get; set; }
        [StringLength(50)]
        [Required]
        [Display(Name = "Address")]
        [RegularExpression("^[A-Z][a-z]+$", ErrorMessage = "Frist latter should be capital for every word")]
        public string Address2 { get; set; } 
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        [RegularExpression("^[A-Z][a-z]+$", ErrorMessage = "Frist latter should be capital for every word")]
        public string Street { get; set; } 
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        [RegularExpression("^[A-Z][a-z]+$", ErrorMessage = "Frist latter should be capital for every word")]
        public string City { get; set; }
        [Required]
        [RegularExpression("^\\d{5,6}",ErrorMessage ="1.It is vaild only Numbers \n 2.Zipcode length should be in the ranfe of 5 to 6")]
        public long ZipCode { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("FkstateId")]
        [InverseProperty("AddressBooks")]
        [Display(Name ="State")]
        public virtual State? Fkstate { get; set; } = null!;
        [ForeignKey("FkuserId")]
        [InverseProperty("AddressBooks")]
        [Display(Name ="UserName")]
        public virtual UserDetail? Fkuser { get; set; } = null!;
    }
}
