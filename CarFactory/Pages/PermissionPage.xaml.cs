using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class PermissionPage : Page
    {
        public PermissionPage()
        {
            InitializeComponent();

            RoleComboBox.ItemsSource = App.Connection.Roles.ToList();
            PermissionComboBox.ItemsSource = App.Connection.ConstructionSteps.ToList();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void AddPermissionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(RoleComboBox.SelectedItem is Role role) ||
                !(PermissionComboBox.SelectedItem is ConstructionStep constructionStep))
            {
                MessageBox.Show("Choose all fields");
                return;
            }

            if (App.Connection.RolePermissions.FirstOrDefault(x =>
                    x.Role.Id == role.Id && x.ConstructionStep.Id == constructionStep.Id) != null)
            {
                MessageBox.Show("Role already has this permission");
                return;
            }

            var permission = new RolePermission { Role = role, ConstructionStep = constructionStep };
            App.Connection.RolePermissions.Add(permission);
            App.Connection.SaveChanges();
            MessageBox.Show("Successfully added new permission");
            PermissionComboBox.SelectedItem = null;
            UpdateExistingPermissions();
        }

        private void RoleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateExistingPermissions();
        }

        private void UpdateExistingPermissions()
        {
            ExistingPermissionsListView.ItemsSource =
                App.Connection.RolePermissions.Where(x => x.RoleId == ((Role)RoleComboBox.SelectedItem).Id).ToList();
        }
    }
}