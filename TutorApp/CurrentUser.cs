using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp
{
    /// <summary>
    /// Статический класс для хранения информации о текущем авторизованном пользователе
    /// Используется для разделения данных между разными аккаунтами репетиторов
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>Уникальный идентификатор текущего пользователя</summary>
        public static int Id { get; set; }

        /// <summary>Логин текущего пользователя</summary>
        public static string Login { get; set; } = string.Empty;
    }
}
