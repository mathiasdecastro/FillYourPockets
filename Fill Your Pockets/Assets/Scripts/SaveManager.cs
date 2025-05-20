using UnityEngine;
using System.IO;
using UnityEngine.Rendering.Universal.Internal;
using System;

public class SaveManager : MonoBehaviour
{
    private static string saveFileNamePath = Application.persistentDataPath + "/save.json";

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFileNamePath, json);
        Debug.Log("Game saved to " + saveFileNamePath);
    }

    public static GameData Load()
    {
        if (File.Exists(saveFileNamePath))
        {
            string json = File.ReadAllText(saveFileNamePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from " + saveFileNamePath);

            return data;
        }
        else
        {
            Debug.Log("No save file found at " + saveFileNamePath);
            return null;
        }
    }
}