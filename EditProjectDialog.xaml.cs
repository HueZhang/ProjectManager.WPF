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
    /// EditProjectDialog.xaml 的交互逻辑
    /// </summary>
    public partial class EditProjectDialog : Window
    {

        public string EditedProjectName => EditedProjectNameBox.Text.Trim();

        /// <summary>
        /// 显示旧名称
        /// </summary>
        /// <param name="oldName"></param>
        public EditProjectDialog(string oldName)
        {
            InitializeComponent();

            OldProjectNameBox.Text = oldName;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditedProjectName))
            {
                MessageBox.Show("新项目名称不能为空。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
