using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using GoogleMobileAds.Api;



public class ScoreScript : MonoBehaviour
{
    public GameManagerGengo gamemanagergengo;
    public TimeScript TimeScript;
    static public int score = 0;
    public Text kyu;
    public Text Scoreend;
    public Text highScoreText;
    public string key = "HIGH SCORE";


    public void Start() {
        if (gamemanagergengo.isRanking)
        {
            gamemanagergengo.count_quiz_answer_highScore = PlayerPrefs.GetInt(key, 0);
            highScoreText.text = $"最高：{gamemanagergengo.count_quiz_answer_highScore}問";
        }
        else
        {
            highScoreText.text = "";
        }
    }
    // ハイスコアの保存
    public void Save()
    {
        if (gamemanagergengo.count_quiz_answer > gamemanagergengo.count_quiz_answer_highScore)
        {
            //このIDは任意の登録したものを使う
            string leaderboardID = "CgkI_saSq9ECEAIQAA";
            Debug.Log("スコア " + gamemanagergengo.count_quiz_answer + " を次の Leaderboard に報告します。" + leaderboardID);
            //リーダーボードに保存
            Social.ReportScore(gamemanagergengo.count_quiz_answer, leaderboardID, success => {
                Debug.Log(success ? "スコア報告は成功しました" : "スコア報告は失敗しました");
            });
            //ハイスコア更新
            PlayerPrefs.SetInt(key, gamemanagergengo.count_quiz_answer);
        }
    }
    public void kyuText() {
        Scoreend.text = gamemanagergengo.count_quiz_answer.ToString() + "問正解";
        Debug.Log(gamemanagergengo.count_quiz_answer + "count_quiz_answer");
        if (gamemanagergengo.count_quiz_answer == 0)
        {
            kyu.text = "不合格";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[0]);
        }
        if (gamemanagergengo.count_quiz_answer == 1)
        {
            kyu.text = "残念";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[1]);
        }
        if (gamemanagergengo.count_quiz_answer == 2)
        {
            kyu.text = "頑張りましょう";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[2]);
        }
        if (gamemanagergengo.count_quiz_answer == 3)
        {
            kyu.text = "あとちょっとだったね";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[3]);
        }
        if (gamemanagergengo.count_quiz_answer == 4)
        {
            kyu.text = "もう一息";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[4]);
        }
        if (gamemanagergengo.count_quiz_answer == 5)
        {
            kyu.text = "頑張ったね！";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[5]);
        }
        if (gamemanagergengo.count_quiz_answer == 6)
        {
            kyu.text = "すごいすごい！";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[6]);
        }
        if (gamemanagergengo.count_quiz_answer == 7)
        {
            kyu.text = "グッド！";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[7]);
        }
        if (gamemanagergengo.count_quiz_answer == 8)
        {
            kyu.text = "エクセレント！";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[8]);
        }
        if (gamemanagergengo.count_quiz_answer == 9)
        {
            kyu.text = "マーベラス！";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[9]);
        }
        if (gamemanagergengo.count_quiz_answer >= 10)
        {
            kyu.text = "合格";
            gamemanagergengo.seSource.PlayOneShot(gamemanagergengo.seClip[10]);
        }
        StartCoroutine("ShowInter");
    }


    IEnumerator ShowInter()
    {
        yield return new WaitForSeconds(2.0f);
        //      _interstitial.Show();
        gamemanagergengo.isBackButton = true;
        gamemanagergengo.isReplayButton = true;
    }

}