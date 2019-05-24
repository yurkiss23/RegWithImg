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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        private EFContext _context;
        public string ImgStr { get; set; }
        public RegWindow()
        {
            InitializeComponent();
            _context = new EFContext();
            txtFname.Focus();
        }

        public bool IsFill()
        {
            if (string.IsNullOrEmpty(txtFname.Text)
                && string.IsNullOrEmpty(txtLname.Text)
                && string.IsNullOrEmpty(txtEmail.Text)
                && string.IsNullOrEmpty(ImgStr))
            {
                MessageBox.Show("not all fields and images are filled");
            }
            return false;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFill())
            {
                MessageBoxResult result = MessageBox.Show("not all fields and images are filled! try again?", "!!!", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        return;
                    case MessageBoxResult.Cancel:this.Close();
                        return;
                    default:
                        break;
                }
            }

            _context.Users.Add(new User
            {
                FirstName = txtFname.Text,
                LastName = txtLname.Text,
                Email = txtEmail.Text,
                Photo = ImgStr,
                Password = txtPass.Password
            });
            _context.SaveChanges();
            MessageBox.Show("added new user");
            this.Close();
        }

        private void mlbd_Img(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                ImgStr = ImageHelper.ImgToBase64(System.Drawing.Image.FromFile(ofd.FileName));
                imgUser.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
