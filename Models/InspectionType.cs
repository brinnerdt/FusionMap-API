namespace FusionMapAPI.Models
{
    public partial class InspectionType
    {
        public int InspectionId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Cadence { get; set; } = "";
    }
}