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
    /// Логика взаимодействия для окна редактирования данных ученика
    /// </summary>
    public partial class EditStudentWindow : Window
    {
        private Student _student;

        /// <summary>
        /// Инициализирует окно редактирования и заполняет поля текущими данными ученика
        /// </summary>
        /// <param name="student">Объект ученика для редактирования</param>
        public EditStudentWindow(Student student)
        {
            InitializeComponent();
            _student = student;

            txtLastName.Text = _student.LastName;
            txtFirstName.Text = _student.FirstName;
            txtGradeLevel.Text = _student.GradeLevel;
            txtSubject.Text = _student.Subject;
            txtPhone.Text = _student.Phone;
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить"
        /// Обновляет данные ученика в базе
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Фамилия и Имя обязательны для заполнения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var db = new AppDbContext())
                {
                    // Находим ученика в базе по его Id
                    var studentToUpdate = db.Students.Find(_student.Id);

                    if (studentToUpdate != null)
                    {
                        studentToUpdate.LastName = lastName;
                        studentToUpdate.FirstName = firstName;
                        studentToUpdate.GradeLevel = txtGradeLevel.Text.Trim();
                        studentToUpdate.Subject = txtSubject.Text.Trim();
                        studentToUpdate.Phone = txtPhone.Text.Trim();

                        db.SaveChanges(); 
                    }
                }

                MessageBox.Show("Данные обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка");
            }
        }
    }
}