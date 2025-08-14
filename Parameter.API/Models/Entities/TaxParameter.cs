using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Abstractions;

namespace Parameter.API.Models.Entities
{
    // vergi parametreleri sabit
    public class TaxParameter : IHasId<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Key { get; set; } // Örn: "damga_vergisi"

        [Column(TypeName = "decimal(10,5)")]
        public decimal Value { get; set; } // Örn: 0.00759

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
