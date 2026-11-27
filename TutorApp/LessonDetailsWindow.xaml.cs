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
    /// Логика взаимодействия для окна управления занятием
    /// </summary>
    public partial class LessonDetailsWindow : Window
    {
        private Lesson _lesson;

        /// <summary>
        /// Инициализирует окно и заполняет поля текущими данными занятия
        /// </summary>
        /// <param name="lesson">Занятие для редактирования</param>
        public LessonDetailsWindow(Lesson lesson)
        {
            InitializeComponent();
            _lesson = lesson;

            LoadStudents();

            // Заполняем поля
            cbStudents.SelectedValue = _lesson.StudentId;
            dpDate.SelectedDate = _lesson.Date.Date;
            txtHours.Text = _lesson.Date.Hour.ToString("D2");
            txtMinutes.Text = _lesson.Date.Minute.ToString("D2");
            txtDuration.Text = _lesson.Duration.ToString();
            txtTopic.Text = _lesson.Topic;
            cbStatus.SelectedIndex = (int)_lesson.Status;
            txtGrade.Text = _lesson.Grade?.ToString() ?? "";
            txtHomework.Text = _lesson.Homework;
        }

        /// <summary>
        /// Загружает список учеников текущего пользователя
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
        /// Обработчик сохранения изменений занятия
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudents.SelectedItem == null || dpDate.SelectedDate == null || string.IsNullOrEmpty(txtTopic.Text))
            {
                MessageBox.Show("Заполните обязательные поля (Ученик, Дата, Тема)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int hours = int.Parse(txtHours.Text);
                int minutes = int.Parse(txtMinutes.Text);
                DateTime startTime = dpDate.SelectedDate.Value.Date.AddHours(hours).AddMinutes(minutes);
                int duration = int.Parse(txtDuration.Text);
                DateTime endTime = startTime.AddMinutes(duration);

                using (var db = new AppDbContext())
                {
                    var myStudentIds = db.Students.Where(s => s.UserId == CurrentUser.Id).Select(s => s.Id).ToList();

                    var conflictingLesson = db.Lessons
                        .Where(l => myStudentIds.Contains(l.StudentId) && l.Id != _lesson.Id) 
                        .ToList()
                        .FirstOrDefault(l =>
                            (startTime >= l.Date && startTime < l.Date.AddMinutes(l.Duration)) ||
                            (endTime > l.Date && endTime <= l.Date.AddMinutes(l.Duration))
                        );

                    if (conflictingLesson != null)
                    {
                        MessageBox.Show($"На это время уже запланировано занятие!\nНачало в {conflictingLesson.Date:HH:mm}",
                                        "Конфликт расписания", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var lessonToUpdate = db.Lessons.Find(_lesson.Id);
                    if (lessonToUpdate != null)
                    {
                        lessonToUpdate.StudentId = (int)cbStudents.SelectedValue;
                        lessonToUpdate.Date = startTime;
                        lessonToUpdate.Duration = duration;
                        lessonToUpdate.Topic = txtTopic.Text.Trim();
                        lessonToUpdate.Status = (LessonStatus)cbStatus.SelectedIndex;

                        if (string.IsNullOrWhiteSpace(txtGrade.Text))
                            lessonToUpdate.Grade = null;
                        else
                            lessonToUpdate.Grade = int.Parse(txtGrade.Text);

                        lessonToUpdate.Homework = txtHomework.Text.Trim();

                        db.SaveChanges();
                    }
                }

                MessageBox.Show("Данные занятия обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка");
            }
        }
    }
}
