namespace FusionMapAPI.Dtos
{
    public partial class EmployeeDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DOB { get; set; }
        public string Department { get; set; } = "";
        public int Salary { get; set; }
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

    }
}
