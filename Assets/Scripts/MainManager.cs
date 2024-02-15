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
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;

    private string _publicLeaderboardKey =
       "8d9bcd97a20136e5f38205e75af3c4af2efd0e997fd9b642cc8da88c0c0e40cd";

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    class SaveUserName
    {
        public string userName;
    }

    [Serializable]
    class SaveDataScore
    {
        public int totalScore;
    }

    //—охранение имени пользоваетл€
    public void SetUserName(string userName)
    {
        SaveUserName data = new SaveUserName();
        data.userName = userName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveusername.json", json);
    }

    //—охранение всех полученных очков
    public void SetTotalScore(int totalScore)
    {
        SaveDataScore data = new SaveDataScore();
        data.totalScore = totalScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savetotalscore.json", json);
    }

    //ѕолучение имени пользовател€
    public string GetUserName()
    {
        string path = Application.persistentDataPath + "/saveusername.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveUserName data = JsonUtility.FromJson<SaveUserName>(json);
            userName = data.userName;
        }
        return userName;
    }

    //ѕолучение всех очков
    public int GetTotalScore()
    {
        string path = Application.persistentDataPath + "/savetotalscore.json";
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
    public void Setleadrboard(string userName, int totalScore)
    {
        LeaderboardCreator.UploadNewEntry(_publicLeaderboardKey, userName, totalScore);
    }

}
