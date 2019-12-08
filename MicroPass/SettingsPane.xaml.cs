using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace MicroPass
{
    public partial class SettingsPane : Window
    {
        public SettingsPane()
        {
            InitializeComponent();
            this.passwordStorageFolder.Text = PasswordDirectoryManager.GetPossiblyMissingPasswordDirectory();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordDirectoryManager.SetPasswordDirectory(this.passwordStorageFolder.Text);
            this.Close();
        }

        private void PasswordStorageFolder_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            this.fullPathExpansion.Text = Path.GetFullPath(PasswordDirectoryManager.GetComputedPasswordDirectory(this.passwordStorageFolder.Text));
        }
    }
}
