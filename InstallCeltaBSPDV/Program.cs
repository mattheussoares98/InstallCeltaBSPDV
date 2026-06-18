using System.Security.Principal;

namespace InstallCeltaBSPDV
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (!IsRunningAsAdministrator())
            {
                MessageBox.Show(
                    "O aplicativo deve ser executado como administrador. Por favor, clique com o botão direito no aplicativo e escolha 'Executar como administrador'.",
                    "Privilégios de administrador necessários",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new EnableConfigurations());
        }

        private static bool IsRunningAsAdministrator()
        {
            using WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
}