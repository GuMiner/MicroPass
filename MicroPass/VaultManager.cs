using CommonNet.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MicroPass
{
    /// <summary>
    /// Manages all stored passwords
    /// </summary>
    public class VaultManager
    {
        private readonly ITextEncoder textEncoder = new Aes256TextEncoder();

        private readonly string passwordDirectory;

        public VaultManager()
        {
            passwordDirectory = PasswordDirectoryManager.GetOrCreatePasswordDirectory();
        }

        public static string GetAccountFileName(string accountName) => $"{accountName}.txt";

        public IEnumerable<string> GetAccountNames()
        {
            return Directory.GetFiles(passwordDirectory)
               .Select(file => file.Substring(file.LastIndexOf('\\') + 1));
        }

        public string GetAccountDetails(string vaultPassword, string accountName)
        {
            string fileName = Path.Combine(this.passwordDirectory, accountName);
            return textEncoder.DecryptText(File.ReadAllText(fileName, Encoding.UTF8), vaultPassword);
        }

        public void AddAccount(string vaultPassword, string accountName, string accountDetails)
        {
            string filePath = Path.Combine(this.passwordDirectory, VaultManager.GetAccountFileName(accountName));
            if (File.Exists(filePath))
            {
                throw new InvalidOperationException("The account to add already exists. Overwriting existing accounts is not supported yet.");
            }
            else
            {
                File.WriteAllText(filePath, textEncoder.EncryptText(accountDetails, vaultPassword), Encoding.UTF8);
            }
        }
    }
}
