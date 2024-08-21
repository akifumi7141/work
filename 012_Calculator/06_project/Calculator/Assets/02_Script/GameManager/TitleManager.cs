using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.Advertisements;//UnityAds広告
using DG.Tweening;
using TMPro;

public class TitleManager : MonoBehaviour
{
    private GameObject Script;
    BattleManager battleManager;

    public UnityEngine.Audio.AudioMixer mixer;
    public static string Music = "MUSIC";
    public static string SE = "SE";
    public static float fMusic;
    public static float fSE;
    public Slider MusicSlider;
    public Slider SESlider;
    public AudioSource seSource;
    public AudioClip[] seClip;
    public AudioSource MusicSource;
    [SerializeField]
    public AudioClip[] clipBGM;

    public TextMeshPro versionText;//バージョン表示
    public GameObject PresentButton;//プレゼント
    public GameObject noticePanel;

    //パネル
    public GameObject[] AllPanel;//0.設定パネル,1.ゲームオーバー,2.ゲームクリア
    public GameObject MainPanel;//メインパネル
    public GameObject[] GamePanel;//0.ホーム,1.Battle,2.Shop,3.Deck
    public GameObject TogglePanel;//トグル格納パネル
    public GameObject LoadingPanel;//ローディング
    public GameObject ClosedPanel;//ショップ
    public GameObject TextBox;//ショップのテキストボックス
    public string[] line = {"" };
    public GameObject PearentPanel;
    public GameObject ScrollView_Costume;//コスチューム用スクロールビュー
    public RectTransform[] Content_Costume;//コスチューム用コンテンツ
    public GameObject CostumePanel;//コスチューム用パネル
    public GameObject TogglePanel_Costume;//コスチューム用トグルパネル
    public GameObject LoginBonusPanel;//ログボ用パネル
    public GameObject Death_P;//デス数用パネル

    //ボタン
    public GameObject B_LoginBonus;//ログボ用ボタン
    public GameObject B_CostumeGet;//コスチュームGet用ボタン
    public GameObject B_CostumeSave;//コスチューム保存用ボタン
    public GameObject B_ShopChange_R;//ショップ右ボタン
    public GameObject B_ShopChange_L;//ショップ左ボタン
    public GameObject B_ShopUpdate;//アップデートボタン
    public GameObject GameOverBackButton;//ゲームオーバーボタン

    //買い物かご
    public List<int> L_basket1 = new List<int>();
    public List<int> L_basket2 = new List<int>();

    //フィールド
    public GameObject Cube;

    //テキスト
    public TMP_Text successText;

    //カメラ
    public GameObject[] Camera;//0メイン,1.サブスタート,2.対戦,3.ショップ

    //強調表示
    public GameObject[] Emphasispre;

    //Image
    public GameObject image;

    void Start()
    {
        
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        battleManager = Script.GetComponent<BattleManager>();

        //バージョン取得表示
        //        versionText.text = $"Version {Application.version}";
        //事前処理
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat(Music, 0));
        mixer.SetFloat("SEVolume", PlayerPrefs.GetFloat(SE, 0));
        //パネル初期化
        for (int count =0; count < AllPanel.Length; count++)
        {
            AllPanel[count].SetActive(false);
        }
        //プレゼントを10分の1の確率で表示
        int i = Random.Range(0, 10);
        if (i == 5)
        {
            //PresentButton.SetActive(true);
        }

        //トグル初期化
        
        for (int i2 = 0; i2 < GamePanel.Length; i2++)
        {
            if (0 != i2)
            {
                GamePanel[i2].SetActive(false);
            }
            else
            {
                GamePanel[i2].SetActive(true);
            }
        }
        TogglePanel.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
        //ローディング画面非表示
        LoadingPanel.SetActive(false);
        //ショップ閉店
        ClosedPanel.SetActive(true);
        for (int i2 = 0; i2 < 2; i2++)
        {
            battleManager.shop2Value[i2] = ES3.Load<int>(battleManager.playerShopKey[i2], defaultValue: battleManager.shop2Value[i2]);
            int price2 = (battleManager.shop2Price[i2] * battleManager.shop2Value[i2]) + (battleManager.shop2Price[i2] * battleManager.shop2Value[i2]);
            battleManager.ItemsPanel2.transform.GetChild(i2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{price2}";
        }
        //デス数
        int deathCount = ES3.Load<int>("DEATH", defaultValue: 0);
        if (deathCount != 0)
        {
            Death_P.SetActive(true);
            Death_P.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"{deathCount}";
        }
        else
        {
            Death_P.SetActive(false);
        }
        

        //ログボ非表示
        LoginBonusPanel.SetActive(false);
        //コスチュームボタン
        B_CostumeGet.GetComponent<Button>().interactable = false;
        B_CostumeSave.GetComponent<Button>().interactable = false;
        //テキスト非表示
        successText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //====================================================================================================
    //DoTween
    //====================================================================================================
    //拡大表示
    public void dotween_scale(GameObject Obj)
    {
        //セットアップ
        Obj.SetActive(true);
        Obj.GetComponent<CanvasGroup>().alpha = 1.0f;
        Obj.transform.GetChild(0).localScale = Vector3.zero;
        Obj.transform.GetChild(0).DOScale(1f, 0.2f).SetEase(Ease.OutBack);
    }
    //フェードアウト
    public void dotween_fade(GameObject Obj)
    {
        Obj.GetComponent<CanvasGroup>().alpha = 1.0f;
        Obj.GetComponent<CanvasGroup>().DOFade(0.0f, 0.3f).SetEase(Ease.InQuart).OnStepComplete(()
            => Obj.SetActive(false));
    }
    //ボタンタップ強調エフェクト
    public void Button_Emphasis1(GameObject Obj)
    {
        //強調表示
        GameObject Emphasis1;
        GameObject Emphasis2;
        Emphasis1 = (GameObject)Instantiate(Emphasispre[0], Emphasispre[0].transform.position, Emphasispre[0].transform.rotation);//生成
        Emphasis2 = (GameObject)Instantiate(Emphasispre[1], Emphasispre[1].transform.position, Emphasispre[1].transform.rotation);//生成
        Emphasis1.transform.SetParent(Obj.transform, false);//親オブジェクト指定
        Emphasis2.transform.SetParent(Obj.transform, false);//親オブジェクト指定
        //セットアップ
        Emphasis1.transform.localScale = Vector3.zero;
        Emphasis2.transform.localScale = Vector3.zero;
        Emphasis1.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).OnStepComplete(() => dotween_fade_Button(Emphasis1));
        Emphasis2.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).OnStepComplete(() => dotween_fade_Button(Emphasis2));
    }
    public void dotween_fade_Button(GameObject Obj)//ボタン専用
    {
        Obj.GetComponent<CanvasGroup>().alpha = 1.0f;
        Obj.GetComponent<CanvasGroup>().DOFade(0.0f, 0.3f).OnStepComplete(() => GameObject.Destroy(Obj.gameObject));
    }
    //====================================================================================================
    // １文字ずつ表示する演出のコルーチン
    //====================================================================================================
    public IEnumerator ShowCoroutine()
    {
        //テキスト表示
        successText.gameObject.SetActive(true);
        // 待機用コルーチン
        // GC Allocを最小化するためキャッシュしておく
        var delay = new WaitForSeconds(0.05f);

        // テキスト全体の長さ
        var length = successText.text.Length;

        // １文字ずつ表示する演出
        for (var i = 0; i < length; i++)
        {
            // 徐々に表示文字数を増やしていく
            successText.maxVisibleCharacters = i;

            // 一定時間待機
            yield return delay;
        }

        // 演出が終わったら全ての文字を表示する
        successText.maxVisibleCharacters = length;

        //successText = null;
    }
    //挑戦権回復リワード報酬
    public void RewardedAdd()
    {
        StartCoroutine("PanelMove");
        //LoginBonus.loginBonusCount = LoginBonus.loginBonusCount + 3;
        //PlayerPrefs.SetInt(LoginBonus.loginBonus, LoginBonus.loginBonusCount);
        //PlayerPrefs.Save();
        //ChallengingCountText.text = "×" + LoginBonus.ChallengingCount.ToString();
    }
    IEnumerator PanelMove()
    {
        PresentButton.SetActive(false);
        //MissChallenging.SetActive(false);
        noticePanel.SetActive(true);
        noticePanel.transform.DOMoveY(noticePanel.transform.position.y - 2f, 0.3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3);
        noticePanel.transform.DOMoveY(noticePanel.transform.position.y + 2f, 0.3f).SetEase(Ease.Linear).OnComplete(() => noticePanel.SetActive(false));
    }
    public void PlayBgm(AudioClip clip)
    {
        MusicSource.clip = clip;

        if (clip == null)
        {
            MusicSource.clip = null;
            return;
        }

        MusicSource.Play();
    }
}
