using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TutorApp.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? GradeLevel { get; set; }
        public string? Subject { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
