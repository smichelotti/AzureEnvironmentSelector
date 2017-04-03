//------------------------------------------------------------------------------
// <copyright file="EnvSelectorCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Diagnostics;
using System.IO;
using Microsoft.Internal.VisualStudio.PlatformUI;
using EnvDTE;

namespace Michelotti.AzureEnvironmentSelector
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class EnvSelectorCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("38954231-6555-4726-91d3-bd6b1d7a0fd2");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvSelectorCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private EnvSelectorCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.ShowGovModeDialog, menuCommandID);
                //var menuItem = new OleMenuCommand(this.ToggleGovMode, menuCommandID);
                //menuItem.Text = "Toggle Azure Government (start)";
                //menuItem.BeforeQueryStatus += new EventHandler(OnBeforeQueryStatus);
                commandService.AddCommand(menuItem);
            }
        }

        private void ShowGovModeDialog(object sender, EventArgs e)
        {
            // Show dialog
            IVsUIShell uiShell = (IVsUIShell)ServiceProvider.GetService(typeof(SVsUIShell));
            DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE));

            var envSelectorDialog = new EnvSelectorDialog(uiShell, dte);

            //get the owner of this dialog  
            IntPtr hwnd;
            uiShell.GetDialogOwnerHwnd(out hwnd);
            uiShell.EnableModeless(0);
            try
            {
                WindowHelper.ShowModal(envSelectorDialog, hwnd);
            }
            finally
            {
                // This will take place after the window is closed.  
                uiShell.EnableModeless(1);
            }

            if (envSelectorDialog.Restarting)
            {
                string vs = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                string solution = dte.Solution.FullName;
                dte.ExecuteCommand("File.SaveAll");
                dte.ExecuteCommand("File.Exit");
                if (string.IsNullOrEmpty(solution))
                {
                    System.Diagnostics.Process.Start(vs);
                }
                else
                {
                    System.Diagnostics.Process.Start(vs, '"' + solution + '"');
                }
            }
        }

        //private void ToggleGovMode(object sender, EventArgs e)
        //{
        //    //Process proc = new Process();
        //    //proc.StartInfo.FileName = "notepad.exe";
        //    //proc.Start();

        //    // Write the gov metadata file
        //    //var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        //    //var identServicePath = Path.Combine(localAppDataPath, ".IdentityService");
        //    //VerifyFolder(identServicePath);
        //    //var aadConfigPath = Path.Combine(identServicePath, "AadConfigurations");
        //    //VerifyFolder(aadConfigPath);
        //    //var aadProviderConfigFile = Path.Combine(aadConfigPath, "xAadProvider.Configuration.json");
        //    //CreateAadConfigFile(aadProviderConfigFile);
        //}

        

        //private void OnBeforeQueryStatus(object sender, EventArgs e)
        //{
        //    var toggleCmd = sender as OleMenuCommand;
        //    if (toggleCmd != null)
        //    {
        //        //TODO: check if mode is on/off
        //        toggleCmd.Text = "Toggle Azure Government";
        //        toggleCmd.Checked = !toggleCmd.Checked;
        //    }
        //}

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static EnvSelectorCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new EnvSelectorCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        //private void MenuItemCallback(object sender, EventArgs e)
        //{
        //    string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
        //    string title = "GovToggleCommand";

        //    // Show a message box to prove we were here
        //    VsShellUtilities.ShowMessageBox(
        //        this.ServiceProvider,
        //        message,
        //        title,
        //        OLEMSGICON.OLEMSGICON_INFO,
        //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
        //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        //}
    }
}
