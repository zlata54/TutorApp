using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TutorApp.Models
{
    /// <summary>
    /// Класс, представляющий аккаунт репетитора в системе
    /// </summary>
    public class User
    {
        /// <summary>Уникальный идентификатор пользователя</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Логин для входа в систему</summary>
        [Required]
        [MaxLength(50)]
        public string Login { get; set; } = string.Empty;

        /// <summary>Хеш пароля пользователя (в текущей версии хранится в открытом виде для простоты)</summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}