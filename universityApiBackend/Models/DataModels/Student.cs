﻿using Microsoft.Build.Framework;

namespace universityApiBackend.Models.DataModels
{
    public class Student: BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}