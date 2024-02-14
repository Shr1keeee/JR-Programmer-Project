using UnityEngine;
using System.IO;
using System;
using Dan.Main;
using System.Collections.Generic;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    [SerializeField] string userName;
    [SerializeField] int totalScore;
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> scores;

    private string _publicLeaderboardKey =
       "8d9bcd97a20136e5f38205e75af3c4af2efd0e997fd9b642cc8da88c0c0e40cd";

    private void Awake()
    {


        Instance = this;
        //DontDestroyOnLoad(gameObject);
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

    //загрузка таблицы лидеров
    public void GetLeaderboard()
    {

        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    //создание новой записи в таблице результатов
    public void Setleadrboard(string userName, int score)
    {
        LeaderboardCreator.UploadNewEntry(_publicLeaderboardKey, userName, score);
    }
}
