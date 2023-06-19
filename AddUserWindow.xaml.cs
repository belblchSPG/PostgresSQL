using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private readonly Server _server = new Server("Server=localhost; port=5432; user id = postgres; password = testserver; database = Test;");
        private User _tempUser = new User();
        public AddUserWindow()
        {
            InitializeComponent();
        }
        private void TextBoxInputUserID(object sender, TextChangedEventArgs e)
        {
            if (InputUserID.Text != "")
            {
                _tempUser.SetUserID(Convert.ToInt32(InputUserID.Text));
            }
        }
        private void TextBoxInputFirstName(object sender, TextChangedEventArgs e)
        {
            if (InputFirstName.Text != "")
            {
                _tempUser.SetFirstName(InputFirstName.Text);
            }
        }
        private void TextBoxInputLastName(object sender, TextChangedEventArgs e)
        {
            if (InputLastName.Text != "")
            {
                _tempUser.SetLastName(InputLastName.Text);
            }
        }
        private void TextBoxInputNickname(object sender, TextChangedEventArgs e)
        {
            if (InputNickname.Text != "")
            {
                _tempUser.SetGameName(InputNickname.Text);
            }
        }
        private void TextBoxInputBalance(object sender, TextChangedEventArgs e)
        {
            if (InputUserID.Text != "")
            {
                _tempUser.SetBalance(Convert.ToSingle(InputUserID.Text));
            }
        }
        public void ButtonConfirm(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want add this user?", "Add this user", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _server.AddUserToTable(_tempUser);
                this.Close();
            }
            
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberAndLetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z_]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberBalanceValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
