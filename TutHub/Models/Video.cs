using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TutHub.Models
{
    public class Video
    {
        [Required]
        public int VideoId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        public object[] ToArray()
        {

            return new object[] { VideoId, Name, Description, Tags, CourseId, DateCreated, VideoUrl };
        }

        public void FromArray(IList<object> list)
        {
            VideoId = (int)list[0];
            Name = (string)list[1];
            Description = (string)list[2];
            Tags = (string)list[3];
            CourseId = (int)list[4];
            DateCreated = (DateTime)list[5];
            VideoUrl = (string)list[6];
        }
    }
}
