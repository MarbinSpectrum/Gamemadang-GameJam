using System.IO;
using UnityEngine;

public class SaveData
{
    public int clearStage;
}
public class SaveLoad : Singleton<SaveLoad>
{
    string path ;
    string fileName="save.json" ;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath,fileName);
        Load();
    }
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
