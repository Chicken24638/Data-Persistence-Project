using TMPro;
using UnityEngine;
using System;
using System.IO;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public string playerName;
    public string bestPlayerName;

    public int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    [Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveName(TMP_InputField nameField)
    {
        Debug.Log("Saving player name: " + nameField.text);
        playerName = nameField.text;
    }

    public void SaveBestScore(int score, string name)
    {
        SaveData data = new SaveData();
        data.bestScore = score;
        data.bestPlayerName = name;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestPlayerName = data.bestPlayerName;
        }
    }
}
