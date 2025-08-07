using System.ComponentModel.DataAnnotations;

namespace Parameter.API.DTOs.IncomeTaxBracket
{
    public class UpdateIncomeTaxBracketDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Alt sınır boş olamaz.")]
        [Range(0, double.MaxValue, ErrorMessage = "Alt sınır negatif olamaz.")]
        public decimal LowerLimit { get; set; }

        public decimal? UpperLimit { get; set; }

        [Required(ErrorMessage = "Vergi oranı zorunludur.")]
        public decimal Rate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
