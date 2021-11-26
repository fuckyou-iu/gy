using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для ChancelleryWindow.xaml
    /// </summary>
    public partial class ChancelleryWindow : Window
    {
        ChancelleryEntities db = new ChancelleryEntities();   
        public ChancelleryWindow()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            dataGrid.ItemsSource = db.Unit.ToList();
            cbType.ItemsSource = db.TypeUnit.ToList();
            cbCompany.ItemsSource = db.Company.ToList();
        }

        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text, StringComparison.Ordinal) < 0;
        }

        void ClearTb()
        {
            tbName.Clear();
            tbCount.Clear();
            tbPrice.Clear();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbCount.Text) ||
                string.IsNullOrEmpty(tbPrice.Text))
            {
                MessageBox.Show("Заполните все поля", " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TypeUnit tu = cbType.SelectedItem as TypeUnit;
            Company com = cbCompany.SelectedItem as Company;
            Unit u = new Unit
            {
                NameUnit = tbName.Text,
                TypeUnit = tu.IdTypeUnit,
                Company = com.IdCompany,
                Count = int.Parse(tbCount.Text),
                Price = double.Parse(tbPrice.Text),
                EmployeeId = Saver.Employee
            };
            db.Unit.Add(u);
            db.SaveChanges();
            Load();
            ClearTb();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            ClearTb();
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(dataGrid.SelectedItem is Unit u))
                return;
            tbName.Text = u.NameUnit;
            tbCount.Text = u.Count.ToString();
            tbPrice.Text = u.Price.ToString(CultureInfo.InvariantCulture);
            cbType.SelectedItem = db.TypeUnit.SingleOrDefault(c => c.IdTypeUnit == u.TypeUnit);
            cbCompany.SelectedItem = db.Company.SingleOrDefault(c => c.IdCompany == u.Company);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (!(dataGrid.SelectedItem is Unit u))
                return;
            MessageBoxResult res = MessageBox.Show($"Вы уверены, что хотите удалить {u.NameUnit}?", "Удаление",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                db.Unit.Remove(u);
                db.SaveChanges();
                Load();
            }
            

        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            if (!(dataGrid.SelectedItem is Unit u))
                return;
            TypeUnit tu = cbType.SelectedItem as TypeUnit;
            Company com = cbCompany.SelectedItem as Company;
            u.NameUnit = tbName.Text;
            u.TypeUnit = tu.IdTypeUnit;
            u.Company = com.IdCompany;
            u.Count = int.Parse(tbCount.Text);
            u.Price = double.Parse(tbPrice.Text);
            u.EmployeeId = Saver.Employee;
            db.SaveChanges();
            Load();
            ClearTb();
        }
    }
}
