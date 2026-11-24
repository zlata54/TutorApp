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
        }

        private void MenuBackup_Click(object sender, RoutedEventArgs e) { /* Бэкап */ }
        private void MenuRestore_Click(object sender, RoutedEventArgs e) { /* Восстановление */ }
        private void MenuExit_Click(object sender, RoutedEventArgs e) { Application.Current.Shutdown(); }
        private void MenuReportGrades_Click(object sender, RoutedEventArgs e) { /* Отчет успеваемости */ }
        private void MenuReportDebt_Click(object sender, RoutedEventArgs e) { /* Отчет задолженности */ }
        private void MenuAbout_Click(object sender, RoutedEventArgs e) { /* О программе */ }

        private void SearchStudent_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) { /* Поиск */ }
        private void AddStudent_Click(object sender, RoutedEventArgs e) { /* Добавление */ }
        private void EditStudent_Click(object sender, RoutedEventArgs e) { /* Редактирование */ }
        private void DeleteStudent_Click(object sender, RoutedEventArgs e) { /* Удаление */ }

        private void ScheduleDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { /* Фильтр по дате */ }
        private void AddLesson_Click(object sender, RoutedEventArgs e) { /* Добавление урока */ }
        private void CompleteLesson_Click(object sender, RoutedEventArgs e) { /* Отметка о проведении */ }

        private void AddPayment_Click(object sender, RoutedEventArgs e) { /* Внести оплату */ }
        private void ShowStudentReport_Click(object sender, RoutedEventArgs e) { /* Отчет по ученику */ }
    }
}