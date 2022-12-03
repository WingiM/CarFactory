using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class AddCarPage : Page
    {
        public AddCarPage()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            var fields = new string[] { ModelTb.Text, BrandTb.Text, GenerationTb.Text };
            if (fields.Any(x => string.IsNullOrEmpty(x)) || !int.TryParse(fields[2], out var generation))
            {
                MessageBox.Show("No fields are filled");
                return;
            }

            var automobile = new Automobile
            {
                Model = fields[0],
                Brand = fields[1],
                Generation = generation
            };
            App.Connection.Automobiles.Add(automobile);
            App.Connection.SaveChanges();
        }
    }
}