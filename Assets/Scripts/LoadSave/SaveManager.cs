using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public static SaveManager Instance { get; private set; } = new();

    private static string SavesDirectory => Path.Combine(Application.persistentDataPath, "Save");
    private static string GetFilePath(string name) => Path.Combine(SavesDirectory, $"{name}.json");

    private static readonly string SaveManagerDataName = "saveManagerData";

    public Checkpoint LastCheckpoint { get; private set; }
    public string LastCheckpointName { get; private set; }


    private SaveManager()
    {
        LastCheckpointName = GetSaveManagerData();
    }

    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        LastCheckpoint = checkpoint;

        string checkpointName = checkpoint.GetSaveName();
        if (checkpointName != LastCheckpointName)
        {
            LastCheckpointName = checkpoint.GetSaveName();
            SaveSaveManagerData();
        }
    }


    private string GetSaveManagerData()
    {
        Dictionary<string, object> data = LoadFromFile(SaveManagerDataName);
        if (data == null) return null;

        return data.GetValueOrDefault(SaveManagerDataName) as string;
    }

    private void SaveSaveManagerData()
    {
        Dictionary<string, object> data = new() {
            [SaveManagerDataName] = LastCheckpointName
        };
        SaveToFile(SaveManagerDataName, data);
    }


    public Dictionary<string, object> LoadFromFile(string name)
    {
        string filePath = GetFilePath(name);
        try
        {
            // If the file doesn't exist, return null
            if (!File.Exists(filePath)) return null;

            // Load the file and parse it as JSON
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
        catch (System.Exception e)
        {
            // If there's an error, log it and return null
            Debug.LogError($"Failed to load {name} data: {e.Message}");
            return null;
        }
    }

    public void SaveToFile(string name, Dictionary<string, object> data)
    {
        try
        {
            // Convert the dictionary to JSON
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Save the JSON to the file (creating the directory if it doesn't exist)
            if (!Directory.Exists(SavesDirectory)) Directory.CreateDirectory(SavesDirectory);
            File.WriteAllText(GetFilePath(name), json);
        }
        catch (System.Exception e)
        {
            // If there's an error, log it
            Debug.LogError($"Failed to save {name} data: {e.Message}");
        }
    }


    public void NewSave()
    {
        if (Directory.Exists(SavesDirectory)) Directory.Delete(SavesDirectory, true);
        LastCheckpoint = null;
        LastCheckpointName = null;
    }
}
