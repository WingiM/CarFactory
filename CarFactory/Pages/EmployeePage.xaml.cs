using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarFactory.ADO;

namespace CarFactory.Pages
{
    public partial class EmployeePage : Page
    {
        private readonly List<ConstructionStep> _permissions;

        public EmployeePage()
        {
            InitializeComponent();

            _permissions = App.Connection.RolePermissions
                .Where(x => x.RoleId == App.AuthorizedUser.RoleId)
                .Select(x => x.ConstructionStep)
                .ToList();
            if (_permissions.Count == 0)
                throw new Exception("Logged as a person with no permissions");

            SetConstructions();
        }

        private void SetConstructions()
        {
            var ongoingConstructions = App.Connection.Constructions
                .Where(x => !x.Completed)
                .ToList();

            var availableConstructions = ongoingConstructions
                .Where(x => x.ConstructionProcesses
                    .Select(z => z.ConstructionStep)
                    .Intersect(_permissions)
                    .Count() != _permissions.Count);
            ConstructionsListBox.ItemsSource = availableConstructions.ToList();
        }

        private void ConstructionsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            UpdateConstruction();
        }

        private void UpdateConstruction()
        {
            var selectedConstruction = (Construction)ConstructionsListBox.SelectedItem;
            var completedSteps = GetCompletedStepsCollection();
            if (completedSteps.Count == App.Connection.ConstructionSteps.Count())
            {
                selectedConstruction.Completed = true;
                App.Connection.SaveChanges();
                ConsturctionStepsListView.ItemsSource = null;
                CompletedStepsListView.ItemsSource = null;
                ConsturctionStepsListView.SelectedItem = null;
                SetConstructions();
                return;
            }

            CompletedStepsListView.ItemsSource = completedSteps;

            var steps = _permissions.Select(x => x.Id).Except(App.Connection.Constructions
                    .First(x => x.Id == selectedConstruction.Id)
                    .ConstructionProcesses
                    .Select(x => x.CompletedStep))
                .ToList();
            var permittedSteps = App.Connection.ConstructionSteps
                .Where(x => steps.Contains(x.Id))
                .ToList();
            if (permittedSteps.Count == 0)
            {
                ConsturctionStepsListView.ItemsSource = null;
                CompletedStepsListView.ItemsSource = null;
                ConsturctionStepsListView.SelectedItem = null;
                SetConstructions();
                return;
            }

            ConsturctionStepsListView.ItemsSource = permittedSteps;
        }

        private void CompleteStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ConsturctionStepsListView.SelectedItem is ConstructionStep step))
            {
                MessageBox.Show("Select a step!");
                return;
            }

            var neededSteps = step.ConstructionStepRequirements
                .Select(x => x.ConstructionStep1.StepNumber);
            var completedSteps = GetCompletedStepsCollection();
            if (!neededSteps.All(x => completedSteps.Contains(x)))
            {
                MessageBox.Show("Not all needed steps are completed");
                return;
            }

            var process = new ConstructionProcess
            {
                User = App.AuthorizedUser, DateCompleted = DateTime.Now,
                ConstructionStep = (ConstructionStep)ConsturctionStepsListView.SelectedItem,
                Construction = (Construction)ConstructionsListBox.SelectedItem
            };

            App.Connection.ConstructionProcesses.Add(process);
            App.Connection.SaveChanges();
            MessageBox.Show("Successfully completed!");
            UpdateConstruction();
        }

        private List<int> GetCompletedStepsCollection()
        {
            var selectedConstruction = (Construction)ConstructionsListBox.SelectedItem;
            return App.Connection.Constructions
                .First(x => x.Id == selectedConstruction.Id).ConstructionProcesses
                .Select(x => x.ConstructionStep.StepNumber)
                .ToList();
        }

        private void MyProgressButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MyProgressPage());
        }
    }
}