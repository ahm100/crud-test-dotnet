using MediatR;
using System;

namespace Vehicle.Application.Features.JobPostings.Commands
{
    public class CreateJobPostingCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public int JobAdvertiserId { get; set; }
        public int JobCategoryId { get; set; }
    }
}
