using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TutorApp.Models
{
    /// <summary>
    /// Класс, представляющий ученика репетитора
    /// </summary>
    public class Student
    {
        /// <summary>Уникальный идентификатор ученика</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Идентификатор репетитора, которому принадлежит ученик (для разделения данных)</summary>
        public int UserId { get; set; }

        /// <summary>Фамилия ученика</summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>Имя ученика</summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>Класс обучения (например, "9 класс")</summary>
        public string? GradeLevel { get; set; }

        /// <summary>Предмет, по которому проводятся занятия</summary>
        public string? Subject { get; set; }

        /// <summary>Контактный телефон ученика или его родителей</summary>
        public string? Phone { get; set; }

        /// <summary>Коллекция занятий, связанных с этим учеником</summary>
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        /// <summary>Коллекция оплат, связанных с этим учеником</summary>
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}