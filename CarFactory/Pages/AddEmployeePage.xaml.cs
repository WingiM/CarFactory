using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class AddEmployeePage : Page
    {
        public AddEmployeePage()
        {
            InitializeComponent();

            RoleComboBox.ItemsSource = App.Connection.Roles.ToList();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string[] fields = { LoginTb.Text, PasswordTb.Password, NameTb.Text };
            if (fields.Any(string.IsNullOrEmpty) || RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Not all fields filled");
                return;
            }

            if (App.Connection.Logins.FirstOrDefault(x => x.Login1 == LoginTb.Text) != null)
            {
                MessageBox.Show("Person with same login already exists");
                return;
            }

            var user = new User { Name = fields[2], Role = (Role)RoleComboBox.SelectedItem };
            var login = new Login { Login1 = fields[0], Password = fields[1], User = user };

            App.Connection.Logins.Add(login);
            App.Connection.SaveChanges();
            MessageBox.Show($"Successfully added user {user.Name}");
            NavigationService?.GoBack();
        }
    }
}