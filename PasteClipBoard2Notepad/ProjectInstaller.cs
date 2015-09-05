using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteClipBoard2Notepad
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            MessageBox.Show(@"Service PasteClipBoard2Notepad Installed Successfully! Press Ctrl + Windows + X Anywhere To Activate The Service.", @"Service Installed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void serviceInstaller1_AfterUninstall(object sender, InstallEventArgs e)
        {
            MessageBox.Show(@"Service Uninstalled Successfully.", @"Service Uninstalled", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
