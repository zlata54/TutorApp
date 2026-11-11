using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    namespace TutorApp.Models
{
        public class Payment
        {
            [Key]
            public int Id { get; set; }

            public int StudentId { get; set; }

            [Required]
            public DateTime Date { get; set; } 

            public decimal Amount { get; set; } 

            public int LessonsCount { get; set; } 

            [MaxLength(300)]
            public string Comment { get; set; } 


            [ForeignKey("StudentId")]
            public virtual Student Student { get; set; }
        }
    
}
