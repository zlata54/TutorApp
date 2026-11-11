using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TutorApp.Models
{
        public class User
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string Login { get; set; }

            [Required]
            public string PasswordHash { get; set; }
        }
    
}
