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
using System.Windows.Shapes;

namespace TutorApp
{
    /// <summary>
    /// Логика взаимодействия для окна регистрации нового пользователя
    /// </summary>
    public partial class RegisterWindow : Window
    {
        /// <summary>
        /// Инициализирует новое окно регистрации
        /// </summary>
        public RegisterWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Зарегистрироваться"
        /// Создает нового пользователя в базе данных
        /// </summary>
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Логика будет добавлена позже
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Назад ко входу"
        /// Открывает окно входа и закрывает текущее
        /// </summary>
        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWin = new LoginWindow();
            loginWin.Show();
            this.Close();
        }
    }
}
