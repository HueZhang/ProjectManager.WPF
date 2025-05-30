﻿using System;
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
    /// AddProjectDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddProjectDialog : Window
    {

        public string NewProjectName => NewProjectNameBox.Text.Trim();

        public AddProjectDialog()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = false;
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
            Close();
        }
    }
}
