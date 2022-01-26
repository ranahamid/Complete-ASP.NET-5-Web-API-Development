using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class CountryDTO: CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<HotelDTO> Hotels { get; set; }
    }
    public class CreateCountryDTO
    { 
        [Required]
        [StringLength(100, ErrorMessage = "Country Name is too long.")]
        public string Name { get; set; }
        [Required]
        [StringLength(3, ErrorMessage = "Country Short Name is too long.")]
        public string ShortName { get; set; }
    }
}
