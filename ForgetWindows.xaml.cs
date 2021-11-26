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

namespace WpfApp1_
{
    /// <summary>
    /// Логика взаимодействия для ForgetWindows.xaml
    /// </summary>
    public partial class ForgetWindows : Window
    {
        ChancelleryEntities db = new ChancelleryEntities();
        Employee _emp = new Employee();
        public ForgetWindows()
        {
            InitializeComponent();
            Load();
        }

        void Load()
        {
            _emp = db.Employee.SingleOrDefault(c => c.IdEmployee == Saver.Employee);
            textBlock.Text = _emp.Question1.TextQuestion;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (_emp.Answer.ToLower() != textBox.Text.ToLower())
            {
                MessageBox.Show("Ответ неверный!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            gr.Visibility = Visibility.Visible;

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox.Password) || string.IsNullOrEmpty(passwordBox_Copy.Password))
            {
                MessageBox.Show("Заполните поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (passwordBox.Password != passwordBox_Copy.Password)
            {
                MessageBox.Show("Пароли не совпадают!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _emp.Password = passwordBox.Password;
            db.SaveChanges();
            MessageBox.Show("Пароль успешно изменен!", "Yeahoo", MessageBoxButton.OK, MessageBoxImage.Warning);
            Close();
        }
    }
}
