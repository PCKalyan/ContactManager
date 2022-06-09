using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    [Table("State")]
    public partial class State
    {
        public State()
        {
            AddressBooks = new HashSet<AddressBook>();
        }

        [Key]
        [Column("PKStateId")]
        public int PkstateId { get; set; }
        [Column("FKCountryId")]
        [Display(Name ="Country")]
        
        public int FkcountryId { get; set; }
        [Required]
        [RegularExpression("^([A-Z][a-z]+ ?[A-Z][a-z]+|[A-Z][a-z]+)$", ErrorMessage = "Frist latter should be capital for every word")]
        [Display(Name = "State")]
        public string StateName { get; set; } = null!;
        public bool IsActive { get; set; }

        [ForeignKey("FkcountryId")]
        [InverseProperty("States")]
        [Display(Name = "CountryName")]

        public virtual Country? Fkcountry { get; set; } = null!;
        [InverseProperty("Fkstate")]
        public virtual ICollection<AddressBook>? AddressBooks { get; set; }
    }
}
