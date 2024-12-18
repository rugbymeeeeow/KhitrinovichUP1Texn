﻿using System;
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
    /// Логика взаимодействия для AuthorizePage.xaml
    /// </summary>
    public partial class AuthorizePage : Page
    {
        public AuthorizePage()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(Password.Password))
            {
                MessageBox.Show("Вы заполнили не все поля! Попробуйте ещёр раз");
            }
            else
            {
                string login = Login.Text;
                string password = Password.Password;

                try
                {
                    using (var db = new Entities())
                    {
                        var user = db.Users
                            .AsNoTracking().FirstOrDefault(u => u.Login == login && u.Password == password);
                        if (user == null)
                        {
                            MessageBox.Show("Пользователь не зарегистрирован, проверьте данные ещё раз");
                            return;
                        }
                        MessageBox.Show("Вы успешно авторизовались!");

                        Technik.positionID = ReturnPositionID(user.TechnicianID);
                        switch (Technik.positionID)
                        {
                            case 2:
                                NavigationService.Navigate(new Requests());
                                break;
                            case 3:
                                NavigationService.Navigate(new Requests());
                                break;
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private int ReturnPositionID(int? TechnicianID)
        {
            var db = new Entities();
            var technician = db.Technician.AsNoTracking().FirstOrDefault(u => u.technician_id == TechnicianID);

            return (int)technician.position_id;
        }
    }
}
