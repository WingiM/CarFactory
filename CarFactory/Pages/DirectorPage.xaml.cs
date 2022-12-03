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
        }

        private void CarComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var automobile = (Automobile)CarComboBox.SelectedItem;
            ConstructionsListView.ItemsSource = App.Connection.Constructions
                .Where(x => !x.Completed && x.AutomobileId == automobile.Id).ToList();
            CompletedConstructionsListView.ItemsSource = App.Connection.Constructions
                .Where(x => x.Completed && x.AutomobileId == automobile.Id).ToList();
        }
    }
}