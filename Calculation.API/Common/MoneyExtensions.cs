namespace Calculation.API.Common
{
    public static class MoneyExtensions
    {
        public static decimal R2(this decimal v) =>
          Math.Round(v, 2, MidpointRounding.AwayFromZero);
    }
}
