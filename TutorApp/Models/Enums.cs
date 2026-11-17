using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models
{
    /// <summary>
    /// Перечисление возможных статусов занятия
    /// </summary>
    public enum LessonStatus
    {
        /// <summary>Занятие запланировано, но еще не проведено</summary>
        Planned,

        /// <summary>Занятие проведено, выставлена оценка</summary>
        Completed,

        /// <summary>Занятие отменено</summary>
        Cancelled
    }
}
