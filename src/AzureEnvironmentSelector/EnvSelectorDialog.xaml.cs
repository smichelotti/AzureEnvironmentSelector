using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
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
        internal static readonly Dictionary<string, CloudSetting> cloudSettings = Constants.CloudSettings;
        public EnvSelectorDialog(IVsUIShell shell, EnvDTE.DTE dte)
        {
            this.shell = shell;
            this.dte = dte;
            InitializeComponent();

            this.cbEnv.DataContext = cloudSettings;
            this.cbEnv.ItemsSource = cloudSettings;
            this.lblMode.Content = $"Connected to: {cloudSettings[originalConnection].DisplayName}";
            var index = 0;
            foreach (var item in cbEnv.Items)
            {
                var setting = (KeyValuePair<string, CloudSetting>)item;
                if (setting.Key == originalConnection)
                {
                    this.cbEnv.SelectedIndex = index;
                    break;
                }
                index++;
            }
            this.CheckModeSetUI();
        }

        private void CheckModeSetUI()
        {
            var currentConnection = configFileMgr.GetCurrentConnection();
            if (originalConnection != currentConnection)
            {
                var currentModeText = cloudSettings[currentConnection].DisplayName;
                this.tbResults.Text = $"Pending change to {currentModeText} connection.\nYou must restart Visual Studio for these changes to take effect.";
                this.tbResults.Visibility = Visibility.Visible;
                this.btnRestart.Visibility = Visibility.Visible;
            }
            else
            {
                this.tbResults.Visibility = Visibility.Collapsed;
                this.btnRestart.Visibility = Visibility.Collapsed;
            }
            this.lnkViewConfig.Visibility = (currentConnection == Constants.PublicCloudName ? Visibility.Collapsed : Visibility.Visible);
        }


        private void cbEnv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbEnv.SelectedItem == null)
                return;
            var item = (KeyValuePair<string, CloudSetting>)cbEnv.SelectedItem;
            CloudSetting newsetting = item.Value;
            if (newsetting.Name == "AzureCloud")
            {
                configFileMgr.DeleteAadConfigFile();
            }
            else
            {
                configFileMgr.CreateSovereignCloudConfig(newsetting);
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
