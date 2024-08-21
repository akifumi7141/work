using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class LoginBonus : MonoBehaviour
{
    bool isDateExist;
    int todayInt;
    public Text ChallengingCountText;
    static public int ChallengingCount;
    public static string Challenging = "CHALLENGING";

    void Start()
    {
        DateTime now = DateTime.Now;
        int todayInt = 0;
        ChallengingCount = PlayerPrefs.GetInt(Challenging, 0);

        todayInt = now.Year * 1000 + now.Month * 100 + now.Day;

        if (!PlayerPrefs.HasKey("Date"))
        {
            Debug.Log("Dateというデータが存在しません");
            PlayerPrefs.SetInt("Date", todayInt);
            ChallengingCount = ChallengingCount +3;
            PlayerPrefs.SetInt(Challenging, ChallengingCount);
        }
        else
        {
            if (todayInt - PlayerPrefs.GetInt("Date") > 0)
            {
                PlayerPrefs.SetInt("Date", todayInt);
                ChallengingCount = 3 + ChallengingCount;
                PlayerPrefs.SetInt(Challenging, ChallengingCount);
                Debug.Log("次の日になりました");
            }
            else
            {
                Debug.Log("今日すでにログインしています");
            }
        }
        ChallengingCountText.text = "×" + ChallengingCount.ToString();
    }
}
