using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectManager.WPF
{
    /// <summary>
    /// ProjectSettingsDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectSettingsDialog : Window
    {

        public string SvnPrefix => SvnPrefixBox.Text.Trim();
        public string LocalPathPrefix => LocalPathPrefixBox.Text.Trim();

        public ProjectSettingsDialog(string svnPrefix, string localPathPrefix)
        {
            InitializeComponent();

            SvnPrefixBox.Text = svnPrefix;
            LocalPathPrefixBox.Text = localPathPrefix;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SvnPrefix) || string.IsNullOrWhiteSpace(LocalPathPrefix))
            {
                MessageBox.Show("补全信息！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
