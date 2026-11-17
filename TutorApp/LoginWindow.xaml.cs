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
    /// Логика взаимодействия для окна авторизации (входа в систему)
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Инициализирует новое окно входа
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Войти"
        /// Проверяет учетные данные в базе данных и выполняет вход
        /// </summary>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Логика будет добавлена позже
        }

        /// <summary>
        /// Обработчик нажатия на кнопку перехода к регистрации
        /// Открывает окно регистрации и закрывает текущее
        /// </summary>
        private void btnGoToRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new RegisterWindow();
            regWin.Show();
            this.Close();
        }
    }
}