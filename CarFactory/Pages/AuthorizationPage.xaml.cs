using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTb.Text;
            var password = PasswordTb.Password;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Empty login or password");
                return;
            }

            var account = App.Connection.Logins.FirstOrDefault(x => x.Login1 == login && x.Password == password);
            if (account == null)
            {
                MessageBox.Show("Account not found");
                return;
            }

            var user = App.Connection.Users.First(x => x.Id == account.UserId);
            App.AuthorizedUser = user;
            MessageBox.Show($"Welcome, {user.Name}");
            NavigationService?.Navigate(GetNavigatedPage(user));
        }

        private Page GetNavigatedPage(User user)
        {
            switch (user.RoleId)
            {
                case 1:
                    return new DirectorPage(); 
                default:
                    return new EmployeePage();
            }
        }
    }
}