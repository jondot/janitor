using System.ComponentModel;
using System.Configuration.Install;

namespace paracode.Janitor
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}