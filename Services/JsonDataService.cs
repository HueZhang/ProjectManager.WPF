using System.IO;
using System.Windows.Documents;
using ProjectManager.WPF.Models;

namespace ProjectManager.WPF.Services;

public static class JsonDataService
{

    private const string SettingFileName = "settings.json";
    private const string ProjectsFileName = "projects.json";

    public static List<string> LoadProjectsData()
    {

        var projectsFilePath = Path.Combine(Directory.GetCurrentDirectory(), ProjectsFileName);

        if (!File.Exists(projectsFilePath))
        {
            return new List<string>();
        }

        var json = File.ReadAllText(projectsFilePath);
        return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new ();
    }
    public static void SaveProjectsData(List<string> data)
    {
        var projectsFilePath = Path.Combine(Directory.GetCurrentDirectory(), ProjectsFileName);

        var json = System.Text.Json.JsonSerializer.Serialize(data);
        File.WriteAllText(projectsFilePath, json);
    }


    public static SettingsModel LoadSettingsData()
    {
        var settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), SettingFileName);
        if (!File.Exists(settingsFilePath))
        {
            return new SettingsModel();
        }
        var json = File.ReadAllText(settingsFilePath);
        return System.Text.Json.JsonSerializer.Deserialize<SettingsModel>(json) ?? new SettingsModel();
    }

    public static void SaveSettingsData(SettingsModel settings)
    {
        var settingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), SettingFileName);
        var json = System.Text.Json.JsonSerializer.Serialize(settings);
        File.WriteAllText(settingsFilePath, json);
    }

}