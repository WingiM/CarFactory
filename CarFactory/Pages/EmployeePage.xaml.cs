using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class EmployeePage : Page
    {
        private List<RolePermission> _permissions;
        public EmployeePage()
        {
            InitializeComponent();

            _permissions = App.Connection.RolePermissions.Where(x => x.RoleId == App.AuthorizedUser.RoleId).ToList();
            if (_permissions.Count == 0)
                throw new Exception("Logged as a person with no permissions");
        }
    }
}