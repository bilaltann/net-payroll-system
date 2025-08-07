using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parameter.API.Models.Entities
{

    // gelir vergisi dilimi 
    public class IncomeTaxBracket
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LowerLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UpperLimit { get; set; }  // NULL olabilir

        [Column(TypeName = "decimal(5,4)")]
        public decimal Rate { get; set; } // örneğin 0.15 => %15

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } // NULL => hâlâ geçerli

    }
}
