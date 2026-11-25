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

        private void ScheduleDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { /* Фильтр по дате */ }
        private void AddLesson_Click(object sender, RoutedEventArgs e) { /* Добавление урока */ }
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
    }
}