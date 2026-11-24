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
            string login = txtNewLogin.Text.Trim();
            string pass = txtNewPassword.Password;
            string confirm = txtConfirmPassword.Password;

            // Валидация (проверка ввода)
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (pass != confirm)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (pass.Length < 4)
            {
                MessageBox.Show("Пароль должен быть не менее 4 символов!", "Ошибка");
                return;
            }

            using (var db = new AppDbContext())
            {
                // Проверяем, нет ли уже такого логина
                if (db.Users.Any(u => u.Login == login))
                {
                    MessageBox.Show("Такой логин уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем нового пользователя
                var newUser = new User
                {
                    Login = login,
                    PasswordHash = pass 
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Регистрация успешна! Теперь войдите в систему.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginWindow loginWin = new LoginWindow();
                loginWin.Show();
                this.Close();
            }
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