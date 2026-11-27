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
using TutorApp.Models;
using System.Data.Entity;


namespace TutorApp
{
    /// <summary>
    /// Логика взаимодействия для Главного окна приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Инициализирует новое Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void MenuBackup_Click(object sender, RoutedEventArgs e) { /* Бэкап */ }
        private void MenuRestore_Click(object sender, RoutedEventArgs e) { /* Восстановление */ }
        private void MenuExit_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void MenuReportGrades_Click(object sender, RoutedEventArgs e) { /* Отчет успеваемости */ }
        private void MenuReportDebt_Click(object sender, RoutedEventArgs e) { /* Отчет задолженности */ }
        private void MenuAbout_Click(object sender, RoutedEventArgs e) { /* О программе */ }

        private void SearchStudent_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { /* Поиск */ }
        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            // Создаем окно добавления
            AddStudentWindow addWindow = new AddStudentWindow();

            if (addWindow.ShowDialog() == true)
            {
                LoadStudents();
            }
        }
        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного ученика из таблицы
            Student selectedStudent = StudentsDataGrid.SelectedItem as Student;

            if (selectedStudent == null)
            {
                MessageBox.Show("Выберите ученика для редактирования!", "Внимание");
                return;
            }

            // Открываем окно редактирования, передавая туда ученика
            EditStudentWindow editWindow = new EditStudentWindow(selectedStudent);

            if (editWindow.ShowDialog() == true)
            {
                LoadStudents(); 
            }
        }
        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного ученика
            Student selectedStudent = StudentsDataGrid.SelectedItem as Student;

            if (selectedStudent == null)
            {
                MessageBox.Show("Выберите ученика для удаления!", "Внимание");
                return;
            }

            // Подтверждение удаления
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить ученика {selectedStudent.LastName} {selectedStudent.FirstName}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var db = new AppDbContext())
                    {
                        // Находим ученика в базе и удаляем
                        var studentToDelete = db.Students.Find(selectedStudent.Id);
                        if (studentToDelete != null)
                        {
                            db.Students.Remove(studentToDelete);
                            db.SaveChanges();
                        }
                    }
                    MessageBox.Show("Ученик удален!", "Успех");
                    LoadStudents(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка");
                }
            }
        }


        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddLessonWindow addWindow = new AddLessonWindow();
            if (addWindow.ShowDialog() == true)
            {
                LoadLessons(); 
            }
        }

        private void CompleteLesson_Click(object sender, RoutedEventArgs e) { /* Отметка о проведении */ }

        private void AddPayment_Click(object sender, RoutedEventArgs e) { /* Внести оплату */ }
        private void ShowStudentReport_Click(object sender, RoutedEventArgs e) { /* Отчет по ученику */ }

        /// <summary>
        /// Загружает список учеников текущего пользователя из базы данных в таблицу
        /// </summary>
        private void LoadStudents()
        {
            using (var db = new AppDbContext())
            {
                // Берем учеников только текущего репетитора
                var myStudents = db.Students.Where(s => s.UserId == CurrentUser.Id).ToList();
                StudentsDataGrid.ItemsSource = myStudents;
            }
        }

        /// <summary>
        /// Загружает расписание с учетом выбранных фильтров
        /// </summary>
        private void LoadLessons()
        {
            if (LessonsDataGrid == null || StatusFilterCombo == null) return;

            using (var db = new AppDbContext())
            {
                var myStudentIds = db.Students.Where(s => s.UserId == CurrentUser.Id).Select(s => s.Id).ToList();
                var query = db.Lessons.Include("Student").Where(l => myStudentIds.Contains(l.StudentId));

                // Фильтр по статусу
                if (StatusFilterCombo.SelectedIndex == 1)
                    query = query.Where(l => l.Status == LessonStatus.Planned);
                else if (StatusFilterCombo.SelectedIndex == 2)
                    query = query.Where(l => l.Status == LessonStatus.Completed);
                else if (StatusFilterCombo.SelectedIndex == 3)
                    query = query.Where(l => l.Status == LessonStatus.Cancelled);

                var result = query.OrderBy(l => l.Date).ToList();
                LessonsDataGrid.ItemsSource = result;
            }
        }


        private void StatusFilterCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadLessons();
        }

        private void EditLesson_Click(object sender, RoutedEventArgs e)
        {
            var selectedLesson = LessonsDataGrid.SelectedItem as Lesson;
            if (selectedLesson == null)
            {
                MessageBox.Show("Выберите занятие!", "Внимание");
                return;
            }

            LessonDetailsWindow detailsWindow = new LessonDetailsWindow(selectedLesson);
            if (detailsWindow.ShowDialog() == true)
            {
                LoadLessons(); 
            }
        }

        private void ChangeLessonStatus_Click(object sender, RoutedEventArgs e)
        {
            var selectedLesson = LessonsDataGrid.SelectedItem as Lesson;
            if (selectedLesson == null) return;

            LessonDetailsWindow detailsWindow = new LessonDetailsWindow(selectedLesson);
            if (detailsWindow.ShowDialog() == true)
            {
                LoadLessons();
            }
        }

        private void DeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            var selectedLesson = LessonsDataGrid.SelectedItem as Lesson;
            if (selectedLesson == null)
            {
                MessageBox.Show("Выберите занятие для удаления!", "Внимание");
                return;
            }

            if (MessageBox.Show($"Удалить занятие '{selectedLesson.Topic}'?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var lessonToDelete = db.Lessons.Find(selectedLesson.Id);
                    if (lessonToDelete != null)
                    {
                        db.Lessons.Remove(lessonToDelete);
                        db.SaveChanges();
                    }
                }
                LoadLessons();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLessons();
        }
    }
}