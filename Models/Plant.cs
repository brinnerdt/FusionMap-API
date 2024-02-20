namespace FusionMapAPI.Models
{
    public partial class Plant
    {
        public int PlantId { get; set; }
        public string Name { get; set; } = "";
        public int StateId { get; set; }
        public string Status { get; set; } = "";
        public int ReferenceUnitPower { get; set; }
        public int GrossElectricalCapacity { get; set; }
        public DateTime FirstGridConnection { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}