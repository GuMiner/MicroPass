using System;
using System.Configuration;
using System.IO;

namespace MicroPass
{
    class PasswordDirectoryManager
    {
        private const string PasswordDirectory = "passwordDirectory";

        public static string GetOrCreatePasswordDirectory()
        {
            return GetPasswordDirectoryWithAction(ConfigurationManager.AppSettings[PasswordDirectory], (dir) => Directory.CreateDirectory(dir));
        }

        public static string GetPossiblyMissingPasswordDirectory()
        {
            return GetComputedPasswordDirectory(ConfigurationManager.AppSettings[PasswordDirectory]);
        }

        public static string GetComputedPasswordDirectory(string startingDirectory)
        {
            return GetPasswordDirectoryWithAction(startingDirectory, (_) => { });
        }

        public static void SetPasswordDirectory(string passwordDirectory)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[PasswordDirectory].Value = passwordDirectory;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        private static string GetPasswordDirectoryWithAction(string passwordDirectory, Action<string> directoryNotFoundAction)
        {
            if (string.IsNullOrWhiteSpace(passwordDirectory) || !Directory.Exists(passwordDirectory))
            {
                passwordDirectory = Path.Combine(Directory.GetCurrentDirectory(), passwordDirectory, "passwords");
                directoryNotFoundAction(passwordDirectory);
            }

            return passwordDirectory;
        }
    }
}
