using System.ComponentModel.DataAnnotations;

namespace Parameter.API.DTOs.IncomeTaxBracket
{
    public class CreateIncomeTaxBracketDto
    {
        [Required(ErrorMessage = "Alt sınır boş olamaz.")]
        [Range(0, double.MaxValue, ErrorMessage = "Lower limit negatif olamaz.")]
        public decimal LowerLimit { get; set; }
        public decimal? UpperLimit { get; set; }

        [Required(ErrorMessage = "Vergi oranı zorunludur.")]
        public decimal Rate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
