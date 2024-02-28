namespace FusionMapAPI.Dtos
{
    public partial class ReactorInfoDto
    {
        public int ReactorId { get; set; }

        public string Name { get; set; } = "";
        public int MaxPowerOutput { get; set; }
        public string LeadEngineer { get; set; } = "";

    }
}