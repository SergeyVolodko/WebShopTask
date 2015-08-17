namespace Shop.Domain
{
    public class TotalSummary
    {
        public double VATRate { get; set; }
        public double VAT { get; set; }
        public double TotalIncludingVAT { get; set; }
        public double TotalExcludingVAT { get; set; }
    }
}
