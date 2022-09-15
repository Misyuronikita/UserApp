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
using System.Windows.Media.Animation;

namespace UsersApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            DoubleAnimation btnAnimation = new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(1);
            regButton.BeginAnimation(Button.WidthProperty, btnAnimation);
        }
        private void ButtonRegClick(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();
            string pass2 = passBox2.Password.Trim();
            string email = textBoxEmail.Text.ToLower().Trim();

            if (login.Length < 8)
            {
                textBoxLogin.ToolTip = "Это поле введено некорректно";
                textBoxLogin.Background = Brushes.IndianRed;
            }
            else if (pass.Length < 8)
            {
                passBox.ToolTip = "Длина пароля должна быть больше 8 символов";
                passBox.Background = Brushes.IndianRed;
            }
            else if (pass != pass2)
            {
                passBox2.ToolTip = "Пароли не совпадают";
                passBox2.Background = Brushes.IndianRed;
            }
            else if (email.Length < 10 || !email.Contains("@") || !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Электронная почта должна содержать в себе знаки @ и .";
                textBoxEmail.Background = Brushes.IndianRed;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;

                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;

                passBox2.ToolTip = "";
                passBox2.Background = Brushes.Transparent;

                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;

                User user = new User(login, pass, email);
                db.Users.Add(user);
                db.SaveChanges();

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Hide();
            }
        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }
    }
}
