using System.IO;
using UnityEngine;

[System.Serializable]

public class SaveData
{
    private static string path = Application.persistentDataPath + "/settings.json";
    public static PlayerSettings data = new PlayerSettings();

    public static void Save()
    {
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public static void Load()
    {
        if (File.Exists(path))
        {
            data = JsonUtility.FromJson<PlayerSettings>(File.ReadAllText(path));
        }
        else
        {
            data = new PlayerSettings(); // default
            Save();
        }
    }
}