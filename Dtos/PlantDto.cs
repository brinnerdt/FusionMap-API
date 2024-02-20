namespace FusionMapAPI.Dtos
{
    public partial class PlantDto
    {
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