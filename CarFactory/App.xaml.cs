using CarFactory.ADO;

namespace CarFactory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static CarFactoryEntities Connection = new CarFactoryEntities();
        public static User AuthorizedUser;
    }
}