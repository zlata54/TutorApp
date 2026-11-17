using System.Data.Entity;
using TutorApp.Models;
using SQLite.CodeFirst;

namespace TutorApp
{
    /// <summary>
    /// Контекст базы данных для Entity Framework
    /// Отвечает за создание и подключение к локальной базе данных SQLite
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных, используя строку подключения DefaultConnection
        /// </summary>
        public AppDbContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        /// <summary>Таблица пользователей (репетиторов)</summary>
        public DbSet<User> Users { get; set; }

        /// <summary>Таблица учеников</summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>Таблица занятий</summary>
        public DbSet<Lesson> Lessons { get; set; }

        /// <summary>Таблица оплат</summary>
        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Настраивает модель базы данных и создает её при первом запуске, если она не существует
        /// Использует специальный инициализатор для корректной работы с SQLite
        /// </summary>
        /// <param name="modelBuilder">Построитель модели Entity Framework</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var initializer = new SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<AppDbContext>(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}