using System.IO;
using UnityEngine;

public class SaveData
{
    public int clearStage;
}
public class SaveLoad : Singleton<SaveLoad>
{
    string path =Application.persistentDataPath;

    public void Save()
    {
        SaveData saveData = new SaveData();

        saveData.clearStage = GameManager.Instance.ClearStage;
        string json = JsonUtility.ToJson(saveData);

        File .WriteAllText(path, json);
    }

    public void Load()
    {
        SaveData saveData = new SaveData();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);

            GameManager.Instance.ClearStage = saveData.clearStage;
        }
    }
}
