namespace FusionMapAPI.Models
{
    public partial class Isotope
    {
        public int IsotopeId { get; set; }
        public string Name { get; set; } = "";
        public string Symbol { get; set; } = "";
        public int HalfLife { get; set; }
        public DateTime DateSynthesized { get; set; }
    }
}