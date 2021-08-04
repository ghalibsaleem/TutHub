using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TutHub.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string OwnerId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public object[] ToArray()
        {
            return new object[] { CourseId, CourseName, Description, Tags, OwnerId, DateCreated.ToString("s") };
        }


        public void FromArray(IList<object> lstFields)
        {
            CourseId = (int)lstFields[0];
            CourseName = (string)lstFields[1];
            Description = (string)lstFields[2];
            Tags = (string)lstFields[3];
            OwnerId = (string)lstFields[4];
            DateCreated = (DateTime)lstFields[5];
        }
    }
}
