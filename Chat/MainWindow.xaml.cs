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

namespace Chat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("messege is send!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtFname.Text = Fname;
            txtLname.Text = Lname;
            txtEmail.Text = Email;
        }
    }
}
