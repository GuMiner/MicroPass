using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace MicroPass
{
    public partial class AddNewPane : Window
    {
        private static Regex accountNameValidation = new Regex("^[a-zA-Z1-9 @\\-\\.]*$", RegexOptions.Singleline | RegexOptions.Compiled);

        public AddNewPane()
        {
            InitializeComponent();
        }

        public bool AddedAccount { get; private set; } = false;

        public string Account { get; private set; }

        public string AccountDetails { get; private set; }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Account = newAccountBox.Text;
            this.AccountDetails = newAccountPasswordBox.Text;

            if (string.IsNullOrWhiteSpace(this.Account) || string.IsNullOrWhiteSpace(this.AccountDetails))
            {
                throw new ArgumentException("The account, and password boxes must both be filled in.");
            }
            else if (!accountNameValidation.IsMatch(this.Account))
            {
                throw new ArgumentException("The account must only have alphanumeric characters, spaces, dashes, hyphens, or dots in the name.");
            }

            this.AddedAccount = true;
            this.Close();
        }
    }
}
