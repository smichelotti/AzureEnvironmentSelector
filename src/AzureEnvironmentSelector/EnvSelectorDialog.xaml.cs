using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.IO;
using System.Windows;

namespace Michelotti.AzureEnvironmentSelector
{
    /// <summary>
    /// Interaction logic for EnvSelectorDialog.xaml
    /// </summary>
    public partial class EnvSelectorDialog : Window
    {
        private IVsUIShell shell;
        private EnvDTE.DTE dte;
        public bool Restarting { get; private set; } = false;
        private static ConfigFileManager configFileMgr = new ConfigFileManager();
        private static readonly string originalConnection = configFileMgr.GetCurrentConnection();

        public EnvSelectorDialog(IVsUIShell shell, EnvDTE.DTE dte)
        {
            this.shell = shell;
            this.dte = dte;
            InitializeComponent();

            this.lblMode.Content = $"Connected to: {originalConnection}";
            this.CheckModeSetUI();
        }

        private void CheckModeSetUI()
        {
            var currentConnection = configFileMgr.GetCurrentConnection();
            this.cbEnv.SelectedValue = currentConnection;

            if (originalConnection != currentConnection)
            {
                var currentModeText = currentConnection;
                this.tbResults.Text = $"Pending change to {currentModeText} connection.\nYou must restart Visual Studio for these changes to take effect.";
                this.tbResults.Visibility = Visibility.Visible;
                this.btnRestart.Visibility = Visibility.Visible;
                this.cbEnv.SelectedValue = currentModeText;
            }
            else
            {
                this.tbResults.Visibility = Visibility.Collapsed;
                this.btnRestart.Visibility = Visibility.Collapsed;
            }

            this.lnkViewConfig.Visibility = (currentConnection == Clouds.Azure ? Visibility.Collapsed : Visibility.Visible);
        }

        private void cbEnv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (cbEnv.SelectedValue as string)
            {
                case Clouds.Azure:
                    configFileMgr.DeleteAadConfigFile();
                    break;
                case Clouds.AzureGovernment:
                    configFileMgr.CreateGovConfigFile();
                    break;
                default:

                    break;
            }

            CheckModeSetUI();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            this.Restarting = true;
            this.Close();
            
        }

        private void lnkViewConfig_Click(object sender, RoutedEventArgs e)
        {
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.StartInfo.Arguments = ConfigFileManager.GetConfigFilePath();
            proc.Start();
        }

        private void lnkWhatsHappening_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
