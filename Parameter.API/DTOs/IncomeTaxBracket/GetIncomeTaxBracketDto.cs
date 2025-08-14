namespace Parameter.API.DTOs.IncomeTaxBracket
{
    public class GetIncomeTaxBracketDto
    {
        public Guid Id { get; set; }

        public decimal LowerLimit { get; set; }

        public decimal? UpperLimit { get; set; }

        public decimal Rate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
