using UnityEngine;
using System.IO;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    [SerializeField] string userName;
    [SerializeField] int totalScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    class SaveDataUserName
    {
        public string userName;
        
    }

    [Serializable]
    class SaveDataScore
    {
        public int totalScore;
    }

    //Сохранение имени пользоваетля
    public void SaveUserName(string userName)
    {
        SaveDataUserName data = new SaveDataUserName();
        data.userName = userName;
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/usernamesavefile.json", json);
    }

    //Загрузка имени пользователя
    public string LoadUserName()
    {
        string path = Application.persistentDataPath + "/usernamesavefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataUserName data = JsonUtility.FromJson<SaveDataUserName>(json);
            userName = data.userName;
        }
        return userName;
    }

    //Сохранение очков текущей сессии к сумме предыдущих очков
    public void SaveTotalScoreForAllSession(int scoreForPreviousSession, int score)
    {
        SaveDataScore data = new SaveDataScore();
        data.totalScore = scoreForPreviousSession + score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/totalscoresavefile.json", json);

    }

    //Загрузка всех очков
    public int LoadTotalScoreForAllSession()
    {
        string path = Application.persistentDataPath + "/totalscoresavefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataScore data = JsonUtility.FromJson<SaveDataScore>(json);
            totalScore = data.totalScore;
        }
        return totalScore;
    }
}
