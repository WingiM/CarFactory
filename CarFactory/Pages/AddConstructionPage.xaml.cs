using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class AddConstructionPage : Page
    {
        private DirectorPage _page;
        public AddConstructionPage(DirectorPage directorPage)
        {
            InitializeComponent();
            _page = directorPage;
            CarComboBox.ItemsSource = App.Connection.Automobiles.ToList();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void AddConstructionButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarComboBox.SelectedItem == null || !int.TryParse(CountTb.Text, out var count))
            {
                MessageBox.Show("Something went wrong");
                return;
            }

            var car = (Automobile)CarComboBox.SelectedItem;
            for (int i = 0; i < count; i++)
            {
                var construction = new Construction { Automobile = car };
                App.Connection.Constructions.Add(construction);
            }

            App.Connection.SaveChanges();
            MessageBox.Show("Contstructions added!");
            _page.UpdateConstructions();
            NavigationService?.GoBack();
        }
    }
}