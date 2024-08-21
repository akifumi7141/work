using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class LoginBonus : MonoBehaviour
{
    bool isDateExist;
    int todayInt;

    //スクリプト
    private GameObject Script;
    TitleManager titleManager;
    BattleManager battleManager;

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        battleManager = Script.GetComponent<BattleManager>();

        DateTime now = DateTime.Now;
        int todayInt = 0;
        todayInt = now.Year * 1000 + now.Month * 100 + now.Day;

        if (!PlayerPrefs.HasKey("Date"))
        {
            Debug.Log("Dateというデータが存在しません");
            PlayerPrefs.SetInt("Date", todayInt);
            //ログインボーナス初期化
        }
        else
        {
            if (todayInt - PlayerPrefs.GetInt("Date") > 0)
            {
                PlayerPrefs.SetInt("Date", todayInt);
                //ログインボーナス取得
                ES3.Save<bool>(battleManager.playerFlagKey[4], true);//データセーブ
                Debug.Log("次の日になりました");
            }
            else
            {
                Debug.Log("今日すでにログインしています");
            }
        }
        //ログインボーナス現在の状態取得
        bool LoginBonus_F = ES3.Load<bool>(battleManager.playerFlagKey[4], defaultValue: true);//初めからか判定
        titleManager.B_LoginBonus.GetComponent<Button>().interactable = LoginBonus_F;
    }
}
