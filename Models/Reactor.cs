namespace FusionMapAPI.Models
{
    public partial class Reactor
    {
        public int ReactorId { get; set; }
        public int PlantId { get; set; }
        public int InspectionId { get; set; }
        public int MaxPowerOutput { get; set; }
        public int LeadEngineerId { get; set; }
        public int RadiationLevel { get; set; }
    }
}