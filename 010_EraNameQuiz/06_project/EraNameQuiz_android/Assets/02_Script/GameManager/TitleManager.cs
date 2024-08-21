using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;//UnityAds広告
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public GameObject GamePlay;
    public GameObject PlayPanel;
    public GameObject[] PlayButton_List;
    public GameObject OnePlayPanel;
    public GameObject[] OnePlayButton_List;
    public bool isOnePlayPanel = true;

    const int setup_1 = 1;
    static public int loop;

    public GameObject textBook;
    public GameObject OptionPanel;
    public GameObject MissChallenging;
    public GameObject PresentButton;
    public GameObject AddPanel;

    public UnityEngine.Audio.AudioMixer mixer;
    public static string Music = "MUSIC";
    public static string SE = "SE";
    public static float fMusic;
    public static float fSE;
    public Slider MusicSlider;
    public Slider SESlider;

    public AudioSource seSource;
    public AudioClip[] seClip;

    public bool isReward = false;

    public Text ChallengingCountText;
    public Text versionText;



    // Start is called before the first frame update
    void Start()
    {
        //GooglePlayGameServicesに接続
        /*
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((success) => {
            if (success)
            {
                Debug.Log("Authentication successful");
                Debug.Log(Social.localUser.userName);
                Debug.Log(Social.localUser.id);
            }
            else
            {
                Debug.Log("Authentication failed");
            }
        });
        */
        versionText.text = $"Version {Application.version}";
        //事前処理
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat(Music, 0));
        mixer.SetFloat("SEVolume", PlayerPrefs.GetFloat(SE, 0));
        PlayPanel.SetActive(false);
        OnePlayPanel.SetActive(false);
        loop = setup_1;
        textBook.SetActive(false);
        MissChallenging.SetActive(false);
        PresentButton.SetActive(false);
        OptionPanel.SetActive(false);
        //StartCoroutine("ShowBanner");
        //プレゼントを10分の1の確率で表示
        int i = Random.Range(0, 10);
        if (i == 5)
        {
            PresentButton.SetActive(true);
        }
        ChallengingCountText.text = "×" + LoginBonus.ChallengingCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //挑戦権回復リワード報酬
    public void RewardedAdd()
    {
        StartCoroutine("PanelMove");
        LoginBonus.ChallengingCount = LoginBonus.ChallengingCount + 3;
        PlayerPrefs.SetInt(LoginBonus.Challenging, LoginBonus.ChallengingCount);
        PlayerPrefs.Save();
        ChallengingCountText.text = "×" + LoginBonus.ChallengingCount.ToString();
    }
    IEnumerator PanelMove()
    {
        PresentButton.SetActive(false);
        MissChallenging.SetActive(false);
        AddPanel.SetActive(true);
        AddPanel.transform.DOMoveY(AddPanel.transform.position.y - 2f, 0.3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3);
        AddPanel.transform.DOMoveY(AddPanel.transform.position.y + 2f, 0.3f).SetEase(Ease.Linear).OnComplete(() => AddPanel.SetActive(false));
    }
}
