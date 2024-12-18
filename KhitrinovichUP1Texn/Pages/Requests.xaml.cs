using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KhitrinovichUP1Texn.Pages
{
    /// <summary>
    /// Логика взаимодействия для Requests.xaml
    /// </summary>
    public partial class Requests : Page
    {
        public Requests()
        {
            InitializeComponent();

            DataGridRequests.ItemsSource = Entities.GetContext().Request.ToList();

            var priorityList = Entities.GetContext().Priority.Select(x => x.priority_name).ToList();
            priorityList.Insert(0, "Не выбрано");
            FindForPriority.ItemsSource = priorityList;

            var statusList = Entities.GetContext().Status.Select(x => x.status_name).ToList();
            statusList.Insert(0, "Не выбрано");
            FindForStatus.ItemsSource = statusList;
            FindForPriority.SelectedIndex = 0;
            FindForStatus.SelectedIndex = 0;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRequests(null, 0));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRequests((sender as Button).DataContext as Request, Technik.positionID));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridRequests.ItemsSource = Entities.GetContext().Request.ToList();
            }
        }

        private void FindForNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRequests();
        }

        private void FindForStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRequests();
        }

        private void FindForPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRequests();
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            FindForNumber.Text = string.Empty;
            FindForStatus.SelectedIndex = 0;
            FindForPriority.SelectedIndex = 0;

            UpdateRequests();
        }

        private void UpdateRequests()
        {
            var currentRequests = Entities.GetContext().Request.ToList();

            currentRequests = currentRequests.Where(x => x.request_id.ToString().Contains(FindForNumber.Text)).ToList();

            if (FindForPriority.SelectedIndex != 0)
            {
                currentRequests = currentRequests.Where(x => x.priority_id == FindForPriority.SelectedIndex).ToList();
            }

            if (FindForStatus.SelectedIndex != 0)
            {
                currentRequests = currentRequests.Where(x => x.status_id == FindForStatus.SelectedIndex).ToList();
            }
            DataGridRequests.ItemsSource = currentRequests;
        }

        private int ReturnPriority(string addPriority)
        {
            return Entities.GetContext().Priority.Where(x => x.priority_name == addPriority).Select(u => u.priority_id).FirstOrDefault();
        }

        private int ReturnStatus(string addStatus)
        {
            return Entities.GetContext().Status.Where(x => x.status_name == addStatus).Select(u => u.status_id).FirstOrDefault();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }
    }
}
