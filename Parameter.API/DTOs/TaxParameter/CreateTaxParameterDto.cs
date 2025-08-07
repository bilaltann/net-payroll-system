using System.ComponentModel.DataAnnotations;

namespace Parameter.API.DTOs.TaxParameter
{
    public class CreateTaxParameterDto
    {
        [Required]
        [MaxLength(50)]
        public string Key { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } 
    }
}
