using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static readonly string SaveFileNamePath = Application.persistentDataPath + "/save.json";

    public static void Save(GameData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(SaveFileNamePath, json);
        Debug.Log("Game saved to " + SaveFileNamePath);
    }

    public static GameData Load()
    {
        if (File.Exists(SaveFileNamePath))
        {
            var json = File.ReadAllText(SaveFileNamePath);
            var data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from " + SaveFileNamePath);

            return data;
        }
        else
        {
            Debug.Log("No save file found at " + SaveFileNamePath);
            return null;
        }
    }
}