using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public static SaveManager Instance { get; private set; } = new();

    private static string SavesDirectory => Path.Combine(Application.persistentDataPath, "Save");
    private static string GetFilePath(string name) => Path.Combine(SavesDirectory, $"{name}.json");

    private const string SAVE_MANAGER_DATA_NAME = "SaveManagerData";

    public Checkpoint LastCheckpoint { get; private set; }
    public string LastCheckpointName { get; private set; }


    private SaveManager()
    {
        LastCheckpointName = GetSaveManagerData();
    }

    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        LastCheckpoint = checkpoint;

        string oldSaveName = LastCheckpointName;
        LastCheckpointName = checkpoint == null ? null : checkpoint.GetSaveName();

        if (oldSaveName != LastCheckpointName) SaveSaveManagerData();
    }


    private string GetSaveManagerData()
    {
        Dictionary<string, object> data = LoadFromFile(SAVE_MANAGER_DATA_NAME);
        if (data == null) return null;

        return data.GetValueOrDefault(SAVE_MANAGER_DATA_NAME) as string;
    }

    private void SaveSaveManagerData()
    {
        Dictionary<string, object> data = new() {
            [SAVE_MANAGER_DATA_NAME] = LastCheckpointName
        };
        SaveToFile(SAVE_MANAGER_DATA_NAME, data);
    }


    public Dictionary<string, object> Deserialize(string json) => JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
    public Dictionary<string, object> LoadFromFile(string name)
    {
        string filePath = GetFilePath(name);
        try
        {
            // If the file doesn't exist, return null
            if (!File.Exists(filePath)) return null;

            // Load the file and parse it as JSON
            string json = File.ReadAllText(filePath);
            return Deserialize(json);
        }
        catch (System.Exception e)
        {
            // If there's an error, log it and return null
            Debug.LogError($"Failed to load {name} data: {e.Message}");
            return null;
        }
    }

    public string SaveToFile(string name, Dictionary<string, object> data)
    {
        try
        {
            // Convert the dictionary to JSON
            string json = JsonConvert.SerializeObject(data);

            // Save the JSON to the file (creating the directory if it doesn't exist)
            if (!Directory.Exists(SavesDirectory)) Directory.CreateDirectory(SavesDirectory);
            File.WriteAllText(GetFilePath(name), json);
            return json;
        }
        catch (System.Exception e)
        {
            // If there's an error, log it
            Debug.LogError($"Failed to save {name} data: {e.Message}");
            return null;
        }
    }


#if !UNITY_WEBGL
    public void NewSave()
    {
        if (SaveExists()) Directory.Delete(SavesDirectory, true);
        LastCheckpoint = null;
        LastCheckpointName = null;
    }

    public bool SaveExists() => Directory.Exists(SavesDirectory);

#else

    public void NewSave()
    {
        if (!SaveExists()) return;
        SetLastCheckpoint(null);

        string[] files = Directory.GetFiles(SavesDirectory);
        foreach (string file in files) File.WriteAllText(file, "");
    }

    public bool SaveExists() => LastCheckpointName != null && LastCheckpointName.Length > 0;

#endif
}
