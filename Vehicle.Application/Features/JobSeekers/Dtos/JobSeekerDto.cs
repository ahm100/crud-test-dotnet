
namespace Vehicle.Application.Features.JobSeekers.Dtos
{
    public class JobSeekerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Resume { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public List<string> Skills { get; set; }
        public List<JobSeekerExperienceDto> Experience { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class JobSeekerExperienceDto
    {
        public string Company { get; set; }
        public string Role { get; set; }
        public DateTime StartJobDate { get; set; }
        public DateTime EndJobDate { get; set; }
    }
}
