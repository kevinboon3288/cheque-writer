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

namespace ChequeWriter.Modules.UserModule.Views
{
    /// <summary>
    /// Interaction logic for UserAdderView.xaml
    /// </summary>
    public partial class UserAdderView : UserControl
    {
        public UserAdderView()
        {
            InitializeComponent();

            UserAdderViewModel vm = (UserAdderViewModel)this.DataContext;
            vm.ClearAdderPasswordReceived += ClearPassword;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; 
            }
        }

        private void ClearPassword(object? sender, EventArgs e) 
        {
            UserAdderPasswordBox.Password = null;
        }
    }
}
