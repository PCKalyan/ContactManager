using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    [Index("UserName", Name = "UQ__UserDeta__C9F28456B6B367C8", IsUnique = true)]
    public partial class UserDetail
    {
        public UserDetail()
        {
            AddressBooks = new HashSet<AddressBook>();
        }

        [Key]
        [Column("PKUserId")]
        public int PkuserId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        [RegularExpression("^[A-Z][a-z]+$", ErrorMessage = "Frist latter should be capital for every word")]
        public string UserName { get; set; }
        [Unicode(false)]
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$ ", ErrorMessage = "1.Password must contain at leaset one Upppercase,one Lowercase,one Number and one Special Character(@#$%^&-+=) \n 2.Password Length Should be in the range of 8 to 20")]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
        [StringLength(50)]
        [Unicode(false)]
        [Required]
        [RegularExpression("^([A-Z][a-z]+ ?[A-Z][a-z]+|[A-Z][a-z]+)$", ErrorMessage = "Frist latter should be capital for every word")]
        public string FirstName { get; set; } 
        [StringLength(50)]
        [Unicode(false)]
        [RegularExpression("^([A-Z][a-z]+ ?[A-Z][a-z]+|[A-Z][a-z]+)$", ErrorMessage = "Frist latter should be capital for every word")]
        public string? LastName { get; set; }
        [StringLength(50)]
        [Required]
        [Unicode(false)]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\d{10}$", ErrorMessage = "1.It is vaild only Numbers \n 2.Phone number length should be 10")]
        [StringLength(10)]
        public string PhoneNo { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty("Fkuser")]
        public virtual ICollection<AddressBook>? AddressBooks { get; set; }
    }
}
