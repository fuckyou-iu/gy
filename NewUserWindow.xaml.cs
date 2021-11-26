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
    /// Логика взаимодействия для NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        ChancelleryEntities db= new ChancelleryEntities();
        private bool check = true;
        public NewUserWindow()
        {
            InitializeComponent();
            comboBox.ItemsSource = db.Question.ToList();
        }

        

        private void tbLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLog.Text))
            {
                var uriImageSource = new Uri(@"/WpfApp1_;component/Img/icons8_wrong_pincode.ico", UriKind.RelativeOrAbsolute);
                image.Source = new BitmapImage(uriImageSource);
                check = false;
                return;
            }

            Employee emp = db.Employee.SingleOrDefault(c => c.Login == tbLog.Text);
            if (emp is null)
            {
                var uriImageSource = new Uri(@"/WpfApp1_;component/Img/icons8_validation.ico", UriKind.RelativeOrAbsolute);
                image.Source = new BitmapImage(uriImageSource);
                check = true;
            }
            else
            {
                var uriImageSource = new Uri(@"/WpfApp1_;component/Img/icons8_wrong_pincode.ico", UriKind.RelativeOrAbsolute);
                image.Source = new BitmapImage(uriImageSource);
                check = false;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLog.Text) || string.IsNullOrEmpty(tbAns.Text) ||
                string.IsNullOrEmpty(tbFN.Text) ||
                string.IsNullOrEmpty(tbLN.Text))
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (check is false)
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Question q = comboBox.SelectedItem as Question;
            Employee emp = new Employee
            {
                Password = pbPass.Password,
                Answer = tbAns.Text,
                FirstName = tbFN.Text,
                LastName = tbLN.Text,
                Login = tbLog.Text,
                MiddleName = tbMN.Text,
                Question = q.IdQuestion
            };
            db.Employee.Add(emp);
            db.SaveChanges();
            MessageBox.Show("Пользователь успешно создан!", "Yeahoo", MessageBoxButton.OK, MessageBoxImage.Warning);
            Close();
        }
    }
}
