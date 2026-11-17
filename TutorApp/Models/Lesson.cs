using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorApp.Models
{
    /// <summary>
    /// Класс, представляющий занятие с учеником
    /// </summary>
    public class Lesson
    {
        /// <summary>Уникальный идентификатор занятия</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Идентификатор ученика, с которым проводится занятие (внешний ключ)</summary>
        [Required]
        public int StudentId { get; set; }

        /// <summary>Дата и время проведения занятия</summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>Длительность занятия в минутах</summary>
        public int Duration { get; set; }

        /// <summary>Тема проведенного занятия</summary>
        [MaxLength(200)]
        public string Topic { get; set; } = string.Empty;

        /// <summary>Статус занятия (запланировано, проведено, отменено)</summary>
        [Required]
        public LessonStatus Status { get; set; } = LessonStatus.Planned;

        /// <summary>Оценка, выставленная ученику (может быть пустой, если занятие не проведено)</summary>
        public int? Grade { get; set; }

        /// <summary>Заданное домашнее задание</summary>
        [MaxLength(500)]
        public string Homework { get; set; } = string.Empty;

        /// <summary>Навигационное свойство для связи с учеником</summary>
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; } = null!;
    }
}