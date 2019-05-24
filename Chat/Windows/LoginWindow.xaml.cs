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
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            _context = new EFContext();
            txtEmail.Focus();
        }

        public bool IsFind()
        {
            User tmp = null;
            try
            {
                tmp = _context.Users.Where(u => u.Email == txtEmail.Text)
                    .Where(u => u.Password == txtPass.Password).First();
                UserID = tmp.Id;
                Fname = tmp.FirstName;
                Lname = tmp.LastName;
                Email = tmp.Email;
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
                MessageBoxResult result = MessageBox.Show("user no found! try again?", "!!!", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        this.Close();
                        break;
                    case MessageBoxResult.Yes:
                        return;
                    case MessageBoxResult.No:
                        RegWindow reg = new RegWindow();
                        reg.ShowDialog();
                        this.txtEmail.Text = reg.txtEmail.Text;
                        this.txtPass.Password = reg.txtPass.Password;
                        BtnLogin_Click(sender, e);
                        return;
                    default:
                        break;
                }
            }

            MainWindow main = new MainWindow();
            main.Fname = this.Fname;
            main.Lname = this.Lname;
            main.Email = this.Email;
            main.ShowDialog();
            txtEmail.Text = null;
            txtPass.Password = null;
            imgUser.Source = new BitmapImage(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/480px-No_image_available.svg.png"));
            this.Close();
        }

        private void TxtReg_Click(object sender, MouseButtonEventArgs e)
        {
            RegWindow reg = new RegWindow();
            reg.ShowDialog();
            this.txtEmail.Text = reg.txtEmail.Text;
            this.txtPass.Password = reg.txtPass.Password;
            BtnLogin_Click(sender, e);
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
                imgUser.Source = ImageHelper.BitmapToImageSource(ImageHelper.Base64ToImg(
                    _context.Users.Where(u => u.Id == UserID).First().Photo));
            }
        }
    }
}
