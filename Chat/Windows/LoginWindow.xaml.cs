using Chat.Entities;
using Chat.Helpers;
using Microsoft.Win32;
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

namespace Chat.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private EFContext _context;
        public int UserID { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            _context = new EFContext();
        }

        public bool IsFind()
        {
            User tmp = null;
            try
            {
                tmp = _context.Users.Where(u => u.Email == txtEmail.Text)
                    .Where(u => u.Password == txtPass.Password).First();
                UserID = tmp.Id;
            }
            catch
            {
                return false;
            }
            if (tmp != null)
            {
                return true;
            }
            return false;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFind())
            {
                MessageBox.Show("user no found!");
                RegWindow reg = new RegWindow();
                reg.ShowDialog();
            }
        }

        private void TxtReg_Click(object sender, MouseButtonEventArgs e)
        {
            RegWindow reg = new RegWindow();
            reg.ShowDialog();
        }

        private void SelectionChanged_Email(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                btnLogin.IsEnabled = true;
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (IsFind())
            {
                //MessageBox.Show("!!!");
                imgUser.Source = ImageHelper.BitmapToImageSource(ImageHelper.Base64ToImg(
                    _context.Users.Where(u => u.Id == UserID).First().Photo));
            }
        }
    }
}
