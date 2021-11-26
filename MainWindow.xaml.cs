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

namespace WpfApp1_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChancelleryEntities db = new ChancelleryEntities();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TB1.Text) || string.IsNullOrEmpty(TB2.Password))
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Employee emp = db.Employee.SingleOrDefault(c => c.Login == TB1.Text);
            if (emp is null)
            {
                MessageBox.Show("Такого пользователя не существует", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (emp.Password != TB2.Password)
            {
                MessageBox.Show("Неверный пароль!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Saver.Employee = emp.IdEmployee;
            ChancelleryWindow cw = new ChancelleryWindow();
            cw.Show();
            Close();
        }

       

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(TB1.Text))
            {
                MessageBox.Show("Введите логин", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Employee emp = db.Employee.SingleOrDefault(c => c.Login == TB1.Text);
            if (emp is null)
            {
                MessageBox.Show("Такого пользователя не существует", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Saver.Employee = emp.IdEmployee;
            ForgetWindows fw = new ForgetWindows();
            fw.ShowDialog();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow nw = new NewUserWindow();
            nw.ShowDialog();
        }
    }
}
