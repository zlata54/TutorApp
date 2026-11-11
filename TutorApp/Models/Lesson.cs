using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorApp.Models
{
        public class Lesson
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public int StudentId { get; set; } 

            [Required]
            public DateTime Date { get; set; } 

            public int Duration { get; set; }

            [MaxLength(200)]
            public string Topic { get; set; }

            [Required]
            public LessonStatus Status { get; set; } = LessonStatus.Planned; 

            public int? Grade { get; set; } 

            [MaxLength(500)]
            public string Homework { get; set; }

            [ForeignKey("StudentId")]
            public virtual Student Student { get; set; }
        }
}


