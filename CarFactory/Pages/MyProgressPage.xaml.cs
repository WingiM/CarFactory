using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarFactory.Pages
{
    public partial class MyProgressPage : Page
    {
        public MyProgressPage()
        {
            InitializeComponent();

            CompletedStepsListBox.ItemsSource = App.Connection.ConstructionProcesses
                .Where(x => x.CompletedBy == App.AuthorizedUser.Id)
                .OrderBy(x => x.Construction.Id)
                .ToList();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}