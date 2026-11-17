using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TutorApp
{
    /// <summary>
    /// Логика взаимодействия для главного окна приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализирует новое Главное окно и проверяет подключение к базе данных
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                using (var db = new AppDbContext())
                {
                    var count = db.Students.Count();
                    MessageBox.Show($"База данных готова! Учеников: {count}");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}