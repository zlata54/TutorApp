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
using TutorApp.Models;

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
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка");
                return;
            }

            using (var db = new AppDbContext())
            {
                // Ищем пользователя в базе
                var user = db.Users.FirstOrDefault(u => u.Login == login && u.PasswordHash == password);

                if (user != null)
                {
                    // Запоминаем, кто вошел
                    CurrentUser.Id = user.Id;
                    CurrentUser.Login = user.Login;

                    // Открываем главное окно
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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