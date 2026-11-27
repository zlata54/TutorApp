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
    /// Логика взаимодействия для окна добавления нового занятия
    /// </summary>
    public partial class AddLessonWindow : Window
    {
        /// <summary>
        /// Инициализирует окно и загружает список учеников текущего репетитора
        /// </summary>
        public AddLessonWindow()
        {
            InitializeComponent();
            dpDate.SelectedDate = DateTime.Today;
            LoadStudents();
        }

        /// <summary>
        /// Загружает учеников текущего пользователя в выпадающий список
        /// </summary>
        private void LoadStudents()
        {

            using (var db = new AppDbContext())
            {
                var students = db.Students.Where(s => s.UserId == CurrentUser.Id).ToList();
                cbStudents.ItemsSource = students;
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить". Создает новое занятие в базе данных
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudents.SelectedItem == null || dpDate.SelectedDate == null || string.IsNullOrEmpty(txtTopic.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Получаем время начала занятия
                int hours = int.Parse(txtHours.Text);
                int minutes = int.Parse(txtMinutes.Text);
                DateTime startTime = dpDate.SelectedDate.Value.Date.AddHours(hours).AddMinutes(minutes);

                // Получаем длительность
                int duration = int.Parse(txtDuration.Text);
                DateTime endTime = startTime.AddMinutes(duration);

                using (var db = new AppDbContext())
                {
                    // Ищем любые занятия текущего пользователя, которые пересекаются по времени
                    var myStudentIds = db.Students.Where(s => s.UserId == CurrentUser.Id).Select(s => s.Id).ToList();

                    var conflictingLesson = db.Lessons
                        .Where(l => myStudentIds.Contains(l.StudentId))
                        .ToList() 
                        .FirstOrDefault(l =>
                            (startTime >= l.Date && startTime < l.Date.AddMinutes(l.Duration)) || 
                            (endTime > l.Date && endTime <= l.Date.AddMinutes(l.Duration))      
                        );

                    if (conflictingLesson != null)
                    {
                        MessageBox.Show($"На это время уже запланировано занятие с учеником!\nНачало в {conflictingLesson.Date:HH:mm}",
                                        "Конфликт расписания", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return; 
                    }

                    var selectedStudent = cbStudents.SelectedItem as Student;
                    var newLesson = new Lesson
                    {
                        StudentId = selectedStudent.Id,
                        Date = startTime,
                        Duration = duration,
                        Topic = txtTopic.Text.Trim(),
                        Status = LessonStatus.Planned
                    };

                    db.Lessons.Add(newLesson);
                    db.SaveChanges();
                }

                MessageBox.Show("Занятие добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }
    }
}