using System;

namespace Vehicle.Application.Features.JobPostings.Dtos
{
    public class JobPostingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public int JobAdvertiserId { get; set; }
        public string JobAdvertiserName { get; set; } // To include JobAdvertiser's name
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; } // To include JobCategory's name
    }
}
