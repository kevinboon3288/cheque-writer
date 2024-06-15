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

namespace ChequeWriter.Modules.UserModule.Views
{
    /// <summary>
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : UserControl
    {
        public UserLoginView()
        {
            InitializeComponent();

            UserLoginViewModel vm = (UserLoginViewModel)this.DataContext;
            vm.ClearLoginPasswordReceived += ClearLoginPassword;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; 
            }
        }

        private void ClearLoginPassword(object? sender, EventArgs e)
        {
            UserLoginPasswordBox.Password = null;
        }
    }
}
