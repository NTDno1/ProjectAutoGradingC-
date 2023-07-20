namespace DataAccess.Models
{
    public class UserDTO
    {
        public string Studentid { get; set; } = null!;
        public string studentCode { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public string mssv { get; set; }
        public string className { get; set; } = null!;
        public string classId { get; set; } = null!;
    }
}
