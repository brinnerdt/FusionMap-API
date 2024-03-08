namespace FusionMapAPI.Dtos
{
    public partial class PlantDto
    {
        public int PlantId { get; set; }
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
        public int ReferenceUnitPower { get; set; }
        public int GrossElectricalCapacity { get; set; }
        public DateTime FirstGridConnection { get; set; }
    }
}