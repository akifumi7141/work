using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;


public class TitleMyButton : MonoBehaviour
{

    private GameObject Script;
    TitleManager titleManager;
    RewardedAds rewardedAds;
    bool iTweenMoving = false;
//    public AdMob admob;

    void Start()
    {
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "PlayButton0"://一人でプレイ
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                titleManager.OnePlayPanel.SetActive(true);
                TitleManager.loop = 5;
                Hide_Button();
                break;
            case "PlayButton1"://ランキングプレイ
                if (LoginBonus.ChallengingCount > 0) {
                    titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                    LoginBonus.ChallengingCount = LoginBonus.ChallengingCount - 1;
                    PlayerPrefs.SetInt(LoginBonus.Challenging, LoginBonus.ChallengingCount);
                    PlayerPrefs.Save();
                    TitleManager.loop = 99;
                    SceneNavigator.Instance.Change("03_Main", 0.2f);
                }
                else
                {
                    titleManager.MissChallenging.SetActive(true);
                    titleManager.seSource.PlayOneShot(titleManager.seClip[1]);
                }
                break;
            case "PlayButton2"://教科書
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                titleManager.textBook.SetActive(true);
                iTween.MoveFrom(titleManager.textBook, iTween.Hash("x", 0, "y", 100, "time", 0.3f));
                break;
            case "PlayButton3"://戻るボタン
                titleManager.PlayPanel.SetActive(false);
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);
                break;
            case "GamePlay":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                titleManager.PlayPanel.SetActive(true);
                titleManager.PresentButton.SetActive(false);
                iTween.MoveFrom(titleManager.PlayPanel, iTween.Hash("x", 0, "y", 100, "time", 0.3f));

                break;
            case "Option"://オプションボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                titleManager.OptionPanel.SetActive(true);
                iTween.MoveFrom(titleManager.OptionPanel, iTween.Hash("x", 0, "y", 100, "time", 0.3f));
                break;
            case "Ranking"://リーダーボード表示
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                break;
            case "OnePlayButton1_0":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                SceneNavigator.Instance.Change("03_Main", 0.2f);
                break;
            case "OnePlayButton1_1":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                SceneNavigator.Instance.Change("03_Main", 0.2f);
                break;
            case "OnePlayButton1_2":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                SceneNavigator.Instance.Change("03_Main", 0.2f);
                break;
            case "OnePlayButton1_3":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                SceneNavigator.Instance.Change("03_Main", 0.2f);
                break;
            case "OnePlayButton1_4":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                SceneNavigator.Instance.Change("03_Main", 0.2f);
                break;
            case "OnePlayButton1_5":
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                SceneNavigator.Instance.Change("03_Main", 0.2f);
                break;
            case "OnePlayButton2"://右ボタン
                if (!iTweenMoving)
                {
                    titleManager.seSource.PlayOneShot(titleManager.seClip[2]);
                    iTweenMoving = true;
                    TitleManager.loop = (TitleManager.loop + 5) % 6;
                    Hide_Button();
                    iTween.MoveFrom(titleManager.OnePlayButton_List[TitleManager.loop], iTween.Hash("x", 100, "time", 0.5f,
                "oncomplete", "OnCompleteHandler","oncompletetarget", this.gameObject));
                }
                break;
            case "OnePlayButton3"://左ボタン 
                if (!iTweenMoving)
                {
                    titleManager.seSource.PlayOneShot(titleManager.seClip[2]);
                    iTweenMoving = true;
                    TitleManager.loop = (TitleManager.loop + 1) % 6;
                    Hide_Button();
                    iTween.MoveFrom(titleManager.OnePlayButton_List[TitleManager.loop], iTween.Hash("x", -100, "time", 0.5f,
                "oncomplete", "OnCompleteHandler","oncompletetarget", this.gameObject));
                }
                break;
            case "OnePlayButton4"://戻るボタン
                titleManager.OnePlayPanel.SetActive(false);
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);
                break;
            case "textBookButton"://戻るボタン
                titleManager.textBook.SetActive(false);
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);
                break;
            case "OptionPanelButton"://オプション戻るボタン
                titleManager.OptionPanel.SetActive(false);
                PlayerPrefs.SetFloat(TitleManager.SE, TitleManager.fSE);
                PlayerPrefs.SetFloat(TitleManager.Music, TitleManager.fMusic);
                PlayerPrefs.Save();
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);
                break;
            case "SETestButton"://SEテストボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);
                break;
            case "YesButton"://Yesボタン
                break;
            case "NoButton"://Noボタン
                Debug.Log("押したよ");
                titleManager.MissChallenging.SetActive(false);
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);
                break;
            case "PresentButton"://プレゼントボタン
                break;
            default:
                break;
        }
    }
    void Hide_Button()
    {
        titleManager.OnePlayButton_List[0].SetActive(false);
        titleManager.OnePlayButton_List[1].SetActive(false);
        titleManager.OnePlayButton_List[2].SetActive(false);
        titleManager.OnePlayButton_List[3].SetActive(false);
        titleManager.OnePlayButton_List[4].SetActive(false);
        titleManager.OnePlayButton_List[5].SetActive(false);
        titleManager.OnePlayButton_List[TitleManager.loop].SetActive(true);

    }
    void OnCompleteHandler()
    {
        iTweenMoving = false;
    }
}
