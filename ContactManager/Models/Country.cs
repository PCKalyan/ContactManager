using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    [Table("Country")]
    [Index("CountryName", Name = "UQ__Country__E056F201C7DE7FEE", IsUnique = true)]
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        [Key]
        [Column("PKCountryId")]
        public int PkcountryId { get; set; }
        [Required]
        [Display(Name ="Country")]
        [RegularExpression("^([A-Z][a-z]+ ?[A-Z][a-z]+|[A-Z][a-z]+)$", ErrorMessage = "Frist latter should be capital for every word")]
        public string CountryName { get; set; }
        [Required]
        [RegularExpression("^\\d{5,6}", ErrorMessage = "1.It is vaild only Numbers \n 2.Phone number length should be 10")]
        public long ZipCodeStart { get; set; }
        [Required]
        [RegularExpression("^\\d{5,6}", ErrorMessage = "1.It is vaild only Numbers \n 2.Phone number length should be 10")]
        public long ZipCodeEnd { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty("Fkcountry")]
        public virtual ICollection<State>? States { get; set; }
    }
}
