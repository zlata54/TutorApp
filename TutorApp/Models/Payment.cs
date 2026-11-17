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
    /// Класс, представляющий финансовую операцию (оплату) ученика
    /// </summary>
    public class Payment
    {
        /// <summary>Уникальный идентификатор оплаты</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Идентификатор ученика, внесшего оплату (внешний ключ)</summary>
        public int StudentId { get; set; }

        /// <summary>Дата внесения оплаты</summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>Сумма оплаты</summary>
        public decimal Amount { get; set; }

        /// <summary>Количество оплаченных занятий</summary>
        public int LessonsCount { get; set; }

        /// <summary>Комментарий к оплате</summary>
        [MaxLength(300)]
        public string Comment { get; set; } = string.Empty;

        /// <summary>Навигационное свойство для связи с учеником</summary>
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; } = null!;
    }
}