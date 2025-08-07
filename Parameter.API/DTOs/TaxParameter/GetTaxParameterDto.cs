namespace Parameter.API.DTOs.TaxParameter
{
    public class GetTaxParameterDto
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public decimal Value { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
