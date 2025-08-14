using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Abstractions;

namespace Parameter.API.Models.Entities
{

    // gelir vergisi dilimi 
    public class IncomeTaxBracket:IHasId<Guid>
    {
        [Key]
        public Guid Id { get; set; }

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
