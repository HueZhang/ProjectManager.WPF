using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.Services;

namespace ProjectManager.WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    private string _svnPrefix;
    [ObservableProperty]
    private string _localPrefix;

    [ObservableProperty]
    private string _projectName;
    [ObservableProperty]
    private string _svnUrl;
    [ObservableProperty]
    private string _localPath;


    [ObservableProperty]
    private ObservableCollection<string> _projectCollection;



    public MainViewModel()
    {
        LoadSettings();
        LoadProject();
    }

    private void LoadSettings()
    {

        var settingsModel = JsonDataService.LoadSettingsData();

        SvnPrefix = settingsModel.SvnPrefix;
        LocalPrefix = settingsModel.LocalPrefix;
    }

    private void LoadProject()
    {

        ProjectCollection = new ObservableCollection<string>(JsonDataService.LoadProjectsData() ?? new List<string>());
    }



    [RelayCommand]
    private void AddProject()
    {

        if (string.IsNullOrWhiteSpace(SvnPrefix) || string.IsNullOrWhiteSpace(LocalPrefix))
        {
            MessageBox.Show("请先设置svn地址和本地地址");
            return;
        }


        var dialog = new AddProjectDialog
        {
            Owner = Application.Current.MainWindow
        };

        if (dialog.ShowDialog() == true)
        {
            var newProjectName = dialog.NewProjectName.Trim();

            if (!string.IsNullOrEmpty(newProjectName))
            {
                ProjectCollection.Add(newProjectName);
                SaveProjects();
            }
            else
            {
                MessageBox.Show("请输入正确信息。");
            }
        }
    }




    [RelayCommand]
    private void EditSettings()
    {
        var dialog = new ProjectSettingsDialog(SvnPrefix, LocalPrefix)
        {
            Owner = Application.Current.MainWindow
        };


        if (dialog.ShowDialog() == true)
        {
            LocalPrefix = dialog.LocalPathPrefix.Trim();
            SvnPrefix = dialog.SvnPrefix.Trim();

            if (!string.IsNullOrEmpty(LocalPrefix) && !string.IsNullOrWhiteSpace(SvnPrefix))
            {

                SaveSettings();
            }
            else
            {
                MessageBox.Show("请输入正确信息。");
            }
        }



    }


    [RelayCommand]
    private void OpenInExplorer(string projectName)
    {
        if (string.IsNullOrWhiteSpace(projectName))
        {
            MessageBox.Show("项目名为空");
            return;
        }


        var path = $"{LocalPrefix}{projectName}";

        if (Directory.Exists(path))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }
        else
        {
            MessageBox.Show("本地路径不存在。");
        }
    }

    [RelayCommand]
    private void OpenInBrowser(string projectName)
    {

        if (string.IsNullOrWhiteSpace(projectName))
        {
            MessageBox.Show("项目名为空");
            return;
        }


        var path = $"{SvnPrefix}{projectName}";

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }
        catch
        {
            MessageBox.Show("url不存在。");
        }

    }



    [RelayCommand]
    private void DeleteProject(string projectName)
    {
        if (string.IsNullOrWhiteSpace(projectName))
        {
            MessageBox.Show("项目名为空");
            return;
        }

        if (!ProjectCollection.Contains(projectName))
        {
            MessageBox.Show("项目不存在。");
            return;
        }

        // 确定删除?
        var result = MessageBox.Show($"确定删除项目 {projectName} 吗？", "确认删除", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        ProjectCollection.Remove(projectName);
        SaveProjects();
    }

    [RelayCommand]
    private void EditProject(string projectName)
    {
        if (string.IsNullOrWhiteSpace(projectName))
        {
            MessageBox.Show("项目名为空");
            return;
        }

        if (!ProjectCollection.Contains(projectName))
        {
            MessageBox.Show("项目不存在。");
            return;
        }

        var editDialog = new EditProjectDialog(projectName)
        {
            Owner = Application.Current.MainWindow
        };


        if (editDialog.ShowDialog() == true)
        {
            var newProjectName = editDialog.EditedProjectName.Trim();
            if (!string.IsNullOrEmpty(newProjectName) && newProjectName != projectName)
            {
                int index = ProjectCollection.IndexOf(projectName);
                if (index >= 0)
                {
                    ProjectCollection[index] = newProjectName;
                    SaveProjects();
                }
            }
            else
            {
                MessageBox.Show("请输入正确信息。");
            }
        }

    }





    #region IO

    private void SaveSettings()
    {
        JsonDataService.SaveSettingsData(new SettingsModel
        {
            LocalPrefix = LocalPrefix,
            SvnPrefix = SvnPrefix
        });
    }


    private void SaveProjects()
    {
        JsonDataService.SaveProjectsData(ProjectCollection.ToList());
    }

    #endregion



}