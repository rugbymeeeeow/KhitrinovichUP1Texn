﻿using KhitrinovichUP1Texn.Pages;
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

namespace KhitrinovichUP1Texn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = page.Title;

            if (page is Pages.AuthorizePage)
                MenuContainer.Visibility = Visibility.Hidden;
            else MenuContainer.Visibility = Visibility.Visible;

            if (Technik.positionID == 3)
            {
                QR.Visibility = Visibility.Visible;
                Reports.Visibility = Visibility.Collapsed;
            }
            else if (Technik.positionID == 2)
            {
                QR.Visibility = Visibility.Collapsed;
                Reports.Visibility = Visibility.Visible;
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new AuthorizePage());
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Requests());
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ReportPage());
        }

        private void QR_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new QRViewPage());
        }
    }
}

