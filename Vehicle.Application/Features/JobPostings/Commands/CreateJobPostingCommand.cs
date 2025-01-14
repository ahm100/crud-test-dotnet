﻿using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Vehicle.Application.Features.JobPostings.Commands
{
    public class CreateJobPostingCommand : IRequest<int>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ReferenceNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Salary { get; set; }
        [Required]
        public string Location { get; set; }
        public int JobAdvertiserId { get; set; }
        public int JobCategoryId { get; set; }
    }
}
