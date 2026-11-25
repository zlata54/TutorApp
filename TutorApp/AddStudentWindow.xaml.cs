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
    /// Логика взаимодействия для окна добавления нового ученика
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        /// <summary>
        /// Инициализирует новое окно добавления ученика
        /// </summary>
        public AddStudentWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить"
        /// Проверяет ввод и сохраняет нового ученика в базу данных
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string gradeLevel = txtGradeLevel.Text.Trim();
            string subject = txtSubject.Text.Trim();
            string phone = txtPhone.Text.Trim();

            // Валидация (проверка на пустоту)
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Фамилия и Имя обязательны для заполнения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    // Создаем нового ученика и привязываем к текущему репетитору
                    var newStudent = new Student
                    {
                        LastName = lastName,
                        FirstName = firstName,
                        GradeLevel = gradeLevel,
                        Subject = subject,
                        Phone = phone,
                        UserId = CurrentUser.Id // Привязка к тому, кто сейчас вошел
                    };

                    db.Students.Add(newStudent);
                    db.SaveChanges(); 
                }

                MessageBox.Show("Ученик успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; 
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}