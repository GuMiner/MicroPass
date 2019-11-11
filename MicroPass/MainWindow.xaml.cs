using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MicroPass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<string> accounts;
        private readonly VaultManager manager;      

        public MainWindow()
        {
            InitializeComponent();
            manager = new VaultManager();

            accounts = new ObservableCollection<string>();
            foreach (string file in manager.GetAccountNames())
            {
                accounts.Add(file);
            }

            accountsBox.ItemsSource = accounts;
        }

        private void accountsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ValidateVaultPassword();
            if (accountsBox.SelectedItem != null)
            {
                passwordBox.Text = manager.GetAccountDetails(rootPasswordBox.Password, (string)accountsBox.SelectedItem);
            }
        }

        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateVaultPassword();

            AddNewPane addNewPane = new AddNewPane() { Owner = this };
            addNewPane.ShowDialog();
            if (!addNewPane.AddedAccount)
            {
                return;
            }

            manager.AddAccount(rootPasswordBox.Password, addNewPane.Account, addNewPane.AccountDetails);
            if (!accounts.Contains(addNewPane.Account))
            {
                accounts.Add(VaultManager.GetAccountFileName(addNewPane.Account));
            }
        }

        private void ValidateVaultPassword()
        {
            if (string.IsNullOrWhiteSpace(rootPasswordBox.Password))
            {
                throw new ArgumentException("The vault password must be filled in.");
            }
        }
    }
}
