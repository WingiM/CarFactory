using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class DirectorPage : Page
    {
        public DirectorPage()
        {
            InitializeComponent();
            SetItemSources();
            CarComboBox.ItemsSource = App.Connection.Automobiles.ToList();
        }

        public void UpdateConstructions()
        {
            if (CarComboBox.SelectedItem != null)
            {
                UpdateSearch();
                return;
            }

            SetItemSources();
        }

        private void HireEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEmployeePage());
        }

        private void AddNewCarButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddCarPage());
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SetItemSources();
        }

        private void SetItemSources()
        {
            ConstructionsListView.ItemsSource = App.Connection.Constructions.Where(x => !x.Completed).ToList();
            CompletedConstructionsListView.ItemsSource = App.Connection.Constructions.Where(x => x.Completed).ToList();
            CarComboBox.SelectedItem = null;
        }

        private void CarComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            UpdateSearch();
        }

        private void UpdateSearch()
        {
            var automobile = (Automobile)CarComboBox.SelectedItem;
            ConstructionsListView.ItemsSource = App.Connection.Constructions
                .Where(x => !x.Completed && x.AutomobileId == automobile.Id).ToList();
            CompletedConstructionsListView.ItemsSource = App.Connection.Constructions
                .Where(x => x.Completed && x.AutomobileId == automobile.Id).ToList();
        }

        private void AddNewConstruction_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddConstructionPage(this));
        }

        private void GivePermissionToRole_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new PermissionPage());
        }
    }
}