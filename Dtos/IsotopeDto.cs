namespace FusionMapAPI.Dtos
{
    public partial class IsotopeDto
    {
        public string Name { get; set; } = "";
        public string Symbol { get; set; } = "";
        public int HalfLife { get; set; }
        public DateTime DateSynthesized { get; set; }

    }
}