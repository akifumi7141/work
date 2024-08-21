using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Shapes;

public class BattleManager : MonoBehaviour
{
    //スクリプト
    private GameObject Script;
    TitleManager titleManager;
    languageText languagetext;

    //キャンパス
    public Canvas canvas;

    public int key;
    public TextMeshProUGUI formula;//式
    public int formulaSymbol;//計算式に使う記号識別

    public List<int> answerList = new List<int>();//攻撃用リスト
    public GameObject AnswerPanelPre;//アンサープレファブ
    public GameObject fieldContent;//アンサープレファブ格納用コネクト

    //スコア
    public List<int[]> scoreList = new List<int[]>();
    public int[] scoreArray = { 0, 0, 0 };
    public int score = 0;//スコア
    public int scoreClear = 0;//スコアクリア時表示用
    public int highScore = 0;//ハイスコア
    public int scoreAdd = 5;//加算用変数
    public TextMeshProUGUI scoreText;//スコア用テキスト
    public TextMeshProUGUI highScoreText;//ハイスコア用テキスト
    public int[] primeNumber = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
    public string[] evaluation = { "miss", "poor", "nice", "good", "excellent" };//評価
    public TextMeshProUGUI evaluationText;//評価用テキスト

    //コイン
    public int coinClear = 0;//コインクリア時表示用
    public int coinAdd = 5;//加算用変数
    public int consumption;//消費用変数

    //コンボ
    public GameObject ComboPanel;//コンボ格納用パネル
    public TextMeshProUGUI comboText;//コンボ用テキスト
    public int combo = 0;//コンボ用

    //敵データ
    public GameObject[] EnemyPre;//敵
    public GameObject EnemyArea;//敵プレファブ格納用コネクト
    public GameObject GameUnder;//敵プレファブ格納用コネクト
    public List<int> enemyList = new List<int>();//敵リスト
    public int enemyNum = 3;//敵の数
    public int enemyLvMax = 100;//敵の最大値

    //プレイヤーデータ
    public string[] playerDataKey = { "LV", "STAR", "COIN", "SCORE", "LIFE", "DEFENSE" };//レベル,スター,コイン,スコア,ライフ,ディフェンス
    public string[] playerFlagKey = { "START", "KEYLISTCUSTOM", "PURCHASE", "CHARACTER", "LOGINBONUS", "COSTUMEGET" };//
    public string[] playerShopKey = { "LIFE2", "DEFENSE2" };//ライフ,ディフェンス
    public int[] playerDataDefault = { 1, 0, 0, 0, 3, 10 };
    public int[] playerData;//レベル,スター,コイン,スコア,ライフ
    public TextMeshProUGUI[] playerDataText;//テキスト
    public GameObject LifePanel;//ライフパネル
    public GameObject Life_Pre;//ライフプレファブ
    public GameObject Defense_P;//耐久
    public int defeat = 0;//10でクリア
    public GameObject[] character;
    public GameObject topLvImage;//トップのアイコン
    public Sprite[] characterSprite;//メイン画像
    public Sprite[] characterIcon;//アイコン画像
    public int[] characterLoop = { 0, 0, 0, 0, 99, 99, 99, 99 };//キャラクター選択のループ用
    public TextMeshProUGUI[] characterLoopText;//キャラクター選択のループ用
    public bool[][] costumeGet = new bool[4][]; //購入済み判定
    public GameObject characterPre;//スタート用キャラクター
    public GameObject characterHomePre;//ホーム用キャラクター
    public GameObject characterStagePre;//ステージ用キャラクター
    public GameObject characterStageToggle;//ステージ用キャラクタータイプ

    //テンキー
    public GameObject[] KeyListDefault;//初期配置
    public GameObject[] KeyListCustom;//カスタム配置
    public GameObject[] KeyListAll;//全てのキー
    public string[] keyListCustom;//カスタム配置保存用
    public GameObject[] KeyListStart;//スタート時のみ使用(+-*/)
    public GameObject KeyBase;//ベース配列

    //ショップ全18個
    public string[] itemName;//表示名前
    public int[] itemPrice;//値段
    public int[] itemvalue;//初期値
    public GameObject ItemsPanel;//ショップのアイテム表示用
    public GameObject ItemsPanel2;//ショップのアイテム表示用
    public string purchaseTmp_s;//一時格納用
    public string purchaseTmp_s2;//一時格納用
    public int purchaseTmp_i;//一時格納用
    public int purchaseTmp_i2;//一時格納用
    public List<Data_Items> purchaseList = new List<Data_Items>();//購入済みリスト
    public List<GameObject> PurchaseListCustom;//購入済み配置用
    public int[] shop2Price = { 150, 300 };
    public int[] shop2Value = { 1, 1 };
    public int[] shop2Add = { 1, 10 };

    [HideInInspector]
    public List<Data_character> characterList = new List<Data_character>();
    [System.Serializable]
    public class Data_character
    {
        public int No;//No
        public string name;//名前
        public int value1;
        public int value2;
        public Sprite icon;
        public Sprite image;
    }
    [HideInInspector]
    public List<Data_Items> itemsList = new List<Data_Items>();
    [System.Serializable]
    public class Data_Items
    {
        public int No;//No
        public string name;//名前
        public string objname;//オブジェクト名前
        public int price;//値段
        public int value;//持ち数
        public bool custom;//キー配置中か
        public Sprite icon;
        public Sprite image;
    }

    //スタート
    public GameObject SelectPanel;


    #region//スコア判定用
    [System.NonSerialized] public List<int> basePoint = new List<int>();//基礎ポイント
    [System.NonSerialized] public List<int> basePointCount = new List<int>();//クリア時の表示用
    [System.NonSerialized] public List<int> basePoint_L = new List<int>();//スコアカウント用
    public string formulaSymbol_S;//スコアカウント用
    [System.NonSerialized] public List<string> formulaSymbolCount = new List<string>();//スコアカウント用
    [System.NonSerialized] public List<int> formulaSymbolPointCount = new List<int>();//スコアカウント用
    [System.NonSerialized] public List<int> magCount = new List<int>();//スコアカウント用
    [System.NonSerialized] public List<int> formulaSymbolPoint_L = new List<int>();//スコアカウント用
    [System.NonSerialized] public List<string> aanswerCount = new List<string>();//スコアカウント用
    [System.NonSerialized] public List<string> evaCount = new List<string>();//スコアカウント用
    [System.NonSerialized] public List<int> enemyCount = new List<int>();//スコアカウント用
    [System.NonSerialized] public List<int> addCount = new List<int>();//スコアカウント用
    [System.NonSerialized] public List<int> totalCount = new List<int>();//スコアカウント用
    
    public GameObject ScoreCountPre;
    public GameObject ScoreCount_C;
    #endregion

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        languagetext = Script.GetComponent<languageText>();

        //ハイスコア格納
        highScore = ES3.Load<int>("HIGHSCORE", defaultValue: 0);
        highScoreText.text = $"HIGHSCORE:{highScore}";
        //プレイヤーデータ
        for (int i = 0; i < playerData.Length; i++)
        {
            playerData[i] = ES3.Load<int>(playerDataKey[i], defaultValue: playerDataDefault[i]);
            if (playerDataKey[i] == "LIFE")
            {
                for (int i2 = 0; i2 < playerData[i]; i2++)
                {
                    GameObject Obj;
                    Obj = (GameObject)Instantiate(Life_Pre, Life_Pre.transform.position, Quaternion.identity);
                    Obj.transform.SetParent(LifePanel.transform.transform, false);
                }
            }
            else if (playerDataKey[i] == "DEFENSE")
            {
                Defense_P.transform.Find("Slider").GetComponent<Slider>().value = playerData[i];
                Defense_P.transform.Find("Slider/HS_Area/Handle/Text").GetComponent<TextMeshProUGUI>().text = $"{playerData[i]}";
            }
            else if (playerDataKey[i] == "SCORE")
            {
                playerDataText[i].text = $"SCORE:{playerData[i]}";
            }
            //それ場合
            else
            {
                playerDataText[i].text = $"{playerData[i]}";
            }
        }
        //コンボ
        ComboPanel.SetActive(false);
        //トグル
        titleManager.TogglePanel.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
        titleManager.TogglePanel.transform.GetChild(0).GetComponent<Toggle>().interactable = false;

        //キャラクターデータ
        bool playerFlag = ES3.Load<bool>(playerFlagKey[0], defaultValue: false);//初めからか判定
        characterLoop = ES3.Load<int[]>(playerFlagKey[3], defaultValue: new int[8] { 0, 0, 0, 0, 99, 99, 99, 99 });//キャラクター反映
        //ES3.DeleteKey(playerFlagKey[5]);
        costumeGet = ES3.Load<bool[][]>(playerFlagKey[5], defaultValue: new bool[][]{
                    new bool[]{ true, false, false, false, false, false, false, false, false, false, false },
                    new bool[]{ true, false, false, false, false, false, false, false, false, false, false },
                    new bool[]{ true, false, false, false, false },
                    new bool[]{ true, false, false, false, false, false, false, false, false}, });
        //購入済み判定
        for (int j = 0; j < 4; j++ )
        {
            for (int j2 = 0; j2 < costumeGet[j].Length; j2++)
            {
                if (costumeGet[j][j2] == true && j2 != 0)
                {
                    titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(j).GetChild(j2).Find("Panel/Panel2/Panel").gameObject.SetActive(true);
                }
            }
        }
        //コスチューム変更画面非表示
        for (int i3 = 0; i3 < 4; i3++)
        {
            titleManager.TogglePanel_Costume.transform.GetChild(i3).GetComponent<Toggle>().isOn = true;
            //装着中をトグルon
            if (characterLoop[i3 + 4] == 99)
            {
                titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(i3).GetChild(0).GetComponent<Toggle>().isOn = true;
            }
            else
            {
                titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(i3).GetChild(characterLoop[i3 + 4] + 1).GetComponent<Toggle>().isOn = true;
            }
        }
        //コンテンツ
        titleManager.TogglePanel_Costume.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
        titleManager.CostumePanel.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            characterList.Add(new Data_character()
            {
                No = i,
                name = $"name{i}",
                value1 = 0,
                value2 = 0,
                icon = characterIcon[i],
                image = characterSprite[i],
            });
        }
        //ショップ
        for (int i = 0; i < itemName.Length; i++)
        {
            itemsList.Add(new Data_Items()
            {
                No = i,
                name = itemName[i],
                objname = KeyListAll[i].name,
                price = itemPrice[i],
                value = 1,
                icon = null,
                image = null,
            });
        }

        //初めからプレイ
        if (!playerFlag)
        {
            //スタートホームパネル表示
            SelectPanel.SetActive(true);//スタートパネル表示

            //各パネル非表示
            for (int i = 0; i < titleManager.GamePanel.Length; i++)
            {
                titleManager.GamePanel[i].gameObject.SetActive(false);
            }
            titleManager.MainPanel.SetActive(false);

            //カメラ
            titleManager.Camera[1].SetActive(false);
            titleManager.Camera[2].SetActive(true);
            titleManager.Camera[3].SetActive(false);
            titleManager.Camera[4].SetActive(false);
            canvas.worldCamera = titleManager.Camera[2].GetComponent<Camera>();

            //初期キータイプ+
            keyListCustom[1] = KeyListStart[0].name;
            //スキン変更
            characterUpdate(characterPre);
            characterUpdate(characterHomePre);

            for (int i = 0; i < characterLoopText.Length; i++)
            {
                characterLoopText[i].text = $"{characterLoop[i] + 1}";
            }

            // titleManager.GamePanel[0].transform.Find("Start/SelectPanel/Panel/Button1/Image").GetComponent<Image>().sprite = characterList[playerData[5]].image;
            //topLvImage.GetComponent<Image>().sprite = characterList[playerData[5]].icon;
            //TypeText.text = "Type[ + ]";
            //初期キー配置
            for (int i = 0; i < KeyListDefault.Length; i++)
            {
                GameObject Obj;
                Obj = (GameObject)Instantiate(KeyListDefault[i], KeyListDefault[i].transform.position, Quaternion.identity);
                Obj.transform.SetParent(KeyBase.transform.GetChild(i).transform, false);
                keyListCustom[i] = KeyListDefault[i].name;
                //var purchaseDatas = purchaseList.Find(x => x.objname == KeyListDefault[i].name);
                //ColorChange(Obj, purchaseDatas.value);
            }

            //タイプトグル変更
            characterStageToggle.transform.GetChild(characterLoop[3]).GetComponent<Toggle>().isOn = true;
        }
        //途中からプレイ
        else
        {
            //スタートホームパネル非表示
            SelectPanel.SetActive(false);//スタートパネル表示

            //各パネル非表示
            for (int i = 0; i < titleManager.GamePanel.Length; i++)
            {
                if (i == 0)
                {
                    titleManager.GamePanel[i].gameObject.SetActive(true);
                }
                else
                {
                    titleManager.GamePanel[i].gameObject.SetActive(false);
                }
            }
            titleManager.MainPanel.SetActive(true);

            //カメラ
            titleManager.Camera[1].SetActive(false);
            titleManager.Camera[2].SetActive(false);
            titleManager.Camera[3].SetActive(false);
            titleManager.Camera[4].SetActive(true);
            canvas.worldCamera = titleManager.Camera[4].GetComponent<Camera>();

            //topLvImage.GetComponent<Image>().sprite = characterList[playerData[5]].icon;
            //スキン変更
            characterUpdate(characterStagePre);
            characterUpdate(characterHomePre);
            //キー情報更新
            keyUpdate();
        }

    }
    //====================================================================================================
    //キー処理
    //====================================================================================================
    public void keyUpdate()
    {
        //最新情報取得
        if (ES3.KeyExists(playerFlagKey[1]))
            keyListCustom = ES3.Load<string[]>(playerFlagKey[1]);//キー配置
        if (ES3.KeyExists(playerFlagKey[2]))
            purchaseList = ES3.Load<List<Data_Items>>(playerFlagKey[2]);//購入済みリスト

        //初期化
        KeyListCustom = new GameObject[18];
        for (int i = 0; i < KeyListCustom.Length; i++)
        {
            Transform children = KeyBase.transform.GetChild(i).GetComponentInChildren<Transform>();
            //子要素がいなければ終了
            if (children.childCount != 0)
            {
                Destroy(KeyBase.transform.GetChild(i).GetChild(0).gameObject);
            }

        }
        PurchaseListCustom = new List<GameObject>();
        foreach (Transform obj in titleManager.GamePanel[3].transform.Find("ScrollView/Viewport/Content").transform)
        {
            Destroy(obj.gameObject);
        }

        //キー格納
        for (int i = 0; i < keyListCustom.Length; i++)//カスタム
        {
            foreach (var key in KeyListAll)
            {
                if (key.name == keyListCustom[i])
                {
                    KeyListCustom[i] = key.gameObject;//生成用変数に格納
                    keyListCustom[i] = key.name;//保存用変数に格納
                    break;
                }
            }
        }
        for (int i = 0; i < purchaseList.Count; i++)//購入済みキー
        {
            if (purchaseList[i].custom == false)
            {
                foreach (var key in KeyListAll)
                {
                    if (key.name == purchaseList[i].objname)
                    {
                        PurchaseListCustom.Add(key.gameObject);//生成用変数に格納
                        break;
                    }
                }
            }
        }
        //キー配置
        for (int i = 0; i < KeyListCustom.Length; i++)
        {
            if (KeyListCustom[i].name == "B")
            {
                continue;
            }

            //生成
            GameObject Obj;
            Obj = (GameObject)Instantiate(KeyListCustom[i], KeyListCustom[i].transform.position, Quaternion.identity);
            Obj.transform.SetParent(KeyBase.transform.GetChild(i).transform, false);
            Obj.GetComponent<KeyMovement>().enabled = false;
            Obj.name = KeyListCustom[i].name;
            //色変更
            var purchaseDatas = purchaseList.Find(x => x.objname == KeyListCustom[i].name);
            ColorChange(Obj, purchaseDatas.value);

        }
        for (int i = 0; i < PurchaseListCustom.Count; i++)//購入済みキー配置
        {
            //生成
            GameObject Obj;
            Obj = (GameObject)Instantiate(PurchaseListCustom[i], PurchaseListCustom[i].transform.position, Quaternion.identity);
            Obj.transform.SetParent(titleManager.GamePanel[3].transform.Find("ScrollView/Viewport/Content").transform, false);
            Obj.name = PurchaseListCustom[i].name;
            //色変更
            var purchaseDatas = purchaseList.Find(x => x.objname == PurchaseListCustom[i].name);
            ColorChange(Obj, purchaseDatas.value);
        }
    }
    public void keyMovementOff(int count)
    {
        //編集機能On/OFF
        if (count == 3)
        {
            for (int i = 0; i < KeyListCustom.Length; i++)
            {
                //子要素がいなければ終了
                Transform children = KeyBase.transform.GetChild(i).GetComponentInChildren<Transform>();
                if (children.childCount != 0)//子要素がいなければ終了
                {
                    KeyBase.transform.GetChild(i).GetChild(0).GetComponent<KeyMovement>().enabled = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < KeyListCustom.Length; i++)
            {
                //子要素がいなければ終了
                Transform children = KeyBase.transform.GetChild(i).GetComponentInChildren<Transform>();
                if (children.childCount != 0)//子要素がいなければ終了
                {
                    KeyBase.transform.GetChild(i).GetChild(0).GetComponent<KeyMovement>().enabled = false;
                }
            }
        }
    }
    public void characterUpdate(GameObject Obj)
    {
        //ボディ
        for (int i = 0; i < 6; i++)
        {
            if (i == characterLoop[0])
            {
                Obj.transform.Find("Bodies").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("Bodies").GetChild(i).gameObject.SetActive(false);
            }
        }
        //目
        for (int i = 0; i < 11; i++)
        {
            if (i == characterLoop[1])
            {
                Obj.transform.Find("Eyes").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("Eyes").GetChild(i).gameObject.SetActive(false);
            }
        }
        //口鼻
        for (int i = 0; i < 15; i++)
        {
            if (i == characterLoop[2])
            {
                Obj.transform.Find("MouthandNoses").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("MouthandNoses").GetChild(i).gameObject.SetActive(false);
            }
        }
        //ボディパーツ
        for (int i = 0; i < 10; i++)
        {
            if (i == characterLoop[4])
            {
                Obj.transform.Find("Bodyparts").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("Bodyparts").GetChild(i).gameObject.SetActive(false);
            }
        }
        //グローブ
        for (int i = 0; i < 10; i++)
        {
            if (i == characterLoop[5])
            {
                Obj.transform.Find("Gloves").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("Gloves").GetChild(i).gameObject.SetActive(false);
            }
        }
        //ヘッドパーツ
        for (int i = 0; i < 4; i++)
        {
            if (i == characterLoop[6])
            {
                Obj.transform.Find("Headparts").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("Headparts").GetChild(i).gameObject.SetActive(false);
            }
        }
        //尻尾
        for (int i = 0; i < 8; i++)
        {
            if (i == characterLoop[7])
            {
                Obj.transform.Find("Tails").GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Obj.transform.Find("Tails").GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    public void purchaseListUpdate()
    {
        //初期化
        KeyListCustom = new GameObject[18];
        PurchaseListCustom = new List<GameObject>();
        //キー格納
        for (int i = 0; i < KeyListCustom.Length; i++)//カスタム
        {
            Transform children = KeyBase.transform.GetChild(i).transform.GetComponentInChildren<Transform>();
            //子要素がいなければ終了
            if (children.childCount != 0)
            {
                KeyListCustom[i] = KeyBase.transform.GetChild(i).GetChild(0).gameObject;//生成用変数に格納
                keyListCustom[i] = KeyBase.transform.GetChild(i).GetChild(0).name;//保存用変数に格納
                purchaseList.Find(x => x.objname == keyListCustom[i]).custom = true;//配置済みにする
            }
            else
            {
                KeyListCustom[i] = KeyListAll[10].gameObject;//生成用変数にB格納
                keyListCustom[i] = KeyListAll[10].name;//保存用変数にB格納
            }
        }
        GameObject Obj = titleManager.GamePanel[3].transform.Find("ScrollView/Viewport/Content").gameObject;
        for (int i = 0; i < Obj.transform.childCount; i++)
        {
            PurchaseListCustom.Add(Obj.transform.GetChild(i).gameObject);//生成用変数に格納
            purchaseList.Find(x => x.objname == Obj.transform.GetChild(i).gameObject.name).custom = false;//配置済みにする
        }
        //セーブ
        ES3.Save(playerFlagKey[1], keyListCustom);//現在のキーを保存
        ES3.Save(playerFlagKey[2], purchaseList);//購入済みリストセーブ
    }
    public void ColorChange(GameObject Obj,int i)//購入数によって色変更
    {
        if (i == 1)
        {
            Obj.GetComponent<Shape>().settings.outlineColor = new Color(0.4823529f, 0.1568628f, 0.0f, 1.0f);//銅色変更
            Obj.GetComponent<Shape>().settings.fillColor = new Color(1.0f, 0.9411051f, 0.9103774f, 1.0f);//銅色変更
        }
        else if (i == 2)
        {
            Obj.GetComponent<Shape>().settings.outlineColor = new Color(0.6196079f, 0.6745098f, 0.6745098f, 1.0f);//銀色変更
            Obj.GetComponent<Shape>().settings.fillColor = new Color(0.9103774f, 1.0f, 1.0f, 1.0f);//銀色変更
        }
        else if (i == 3)
        {
            Obj.GetComponent<Shape>().settings.outlineColor = new Color(0.7921569f, 0.6588235f, 0.2745098f, 1.0f);//金色変更
            Obj.GetComponent<Shape>().settings.fillColor = new Color(1.0f, 0.9354839f, 0.75f, 1.0f);//金色変更
        }
        else if (i == 4)
        {
            Obj.GetComponent<Shape>().settings.outlineColor = new Color(0.5748898f, 0.2978373f, 0.6509434f, 1.0f);//紫色変更
            Obj.GetComponent<Shape>().settings.fillColor = new Color(0.9153564f, 0.9153564f, 1.0f, 1.0f);//紫色変更
        }
    }
    //====================================================================================================
    //敵の行動処理
    //====================================================================================================
    public void enemyRandom()
    {
        float delay = 0.0f;//ディレイ格納用
        //レベルにより最大数更新
        int enemyLvMaxtmp = enemyLvMax;
        if (playerData[0] >= 20)//プレイヤーレベル20以上で増えていく
        {
            int max = playerData[0] / 10;
            enemyLvMaxtmp = enemyLvMax * max;
        }
        //敵生成
        for (int i = 0; i < enemyNum; i++)
        {
            int rnd = Random.Range(1, enemyLvMaxtmp); // ※ 1～99の範囲でランダムな整数値が返る
            //リスト格納
            enemyList.Add(rnd);
            //生成
            GameObject Obj;
            Obj = (GameObject)Instantiate(EnemyPre[15], EnemyPre[15].transform.position, Quaternion.identity);
            Obj.transform.SetParent(EnemyArea.transform, false);
            Obj.transform.eulerAngles = new Vector3(0, -90, 0);
            Obj.transform.Find("Text").GetComponent<TextMeshPro>().text = $"{enemyList[i]}";
            delay += Random.Range(3.0f, 8.0f);
            StartCoroutine(enemyMoveSide(Obj, delay));
        }
    }
    IEnumerator enemyMoveSide(GameObject Obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Obj != null)
        {
            Obj.transform.DOMoveX(0, 5).SetEase(Ease.Linear)
                .SetLink(Obj).OnComplete(() => StartCoroutine(enemyMoveCenter(Obj)));
        }
    }
    IEnumerator enemyMoveCenter(GameObject Obj)
    {
        Obj.GetComponent<Animator>().SetTrigger("Taunting");
        Obj.transform.eulerAngles = new Vector3(0, 180, 0);
        yield return new WaitForSeconds(2);
        if (Obj != null)
        {
            Obj.transform.DOMoveZ(characterStagePre.transform.parent.position.z, 5).SetEase(Ease.Linear).SetLink(Obj).OnComplete(() => Destroy(Obj, 1f));
        }
    }
    //====================================================================================================
    //ショップ処理
    //====================================================================================================
    public void shopUpdate()
    {
        titleManager.ClosedPanel.SetActive(false);//オープン
        titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"いつもお疲れさま";//セリフ変更
        titleManager.TextBox.transform.Find("PurchaseButtonPanel").gameObject.SetActive(false);//ボタン非表示

        //重複しない用
        List<int> rnd = new List<int>();
        for (int i2 = 0; i2 < itemName.Length; i2++)
        {
            if (i2 != 10)
            {
                rnd.Add(i2);
            }
        }
        while (rnd.Count > 3)
        {

            int index = Random.Range(0, rnd.Count);
            rnd.RemoveAt(index);
        }
        //ショップ並び
        for (int i = 0; i < 3; i++)
        {
            var purchaseDatas = purchaseList.Find(x => x.objname == itemsList[rnd[i]].objname);//購入リストから参照
            int price1 = 1;
            int price2 = 1;
            int color_i = 1;
            ItemsPanel.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Button>().interactable = true;//選択可能
            ItemsPanel.transform.GetChild(i).GetChild(1).GetChild(2).gameObject.SetActive(false);//売り切れ非表示

            //未購入の場合
            if (purchaseDatas == null)
            {
                //値段変更
                price1 = itemsList[rnd[i]].price * itemsList[rnd[i]].value;
                price2 = itemsList[rnd[i]].price * itemsList[rnd[i]].value;
            }
            //購入済みの場合
            else
            {
                //値段変更
                price1 = purchaseDatas.price * purchaseDatas.value;
                price2 = purchaseDatas.price * purchaseDatas.value;
                //上限以上購入済みなら売り切れ
                if (purchaseDatas.value > 3)
                {
                    ItemsPanel.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;//選択不可能
                    ItemsPanel.transform.GetChild(i).GetChild(1).GetChild(2).gameObject.SetActive(true);//売り切れ非表示
                }
                if (15 <= purchaseDatas.No)
                {
                    ItemsPanel.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;//選択不可能
                    ItemsPanel.transform.GetChild(i).GetChild(1).GetChild(2).gameObject.SetActive(true);//売り切れ非表示
                }
                //所持数によって色変更判定変数を変更
                color_i = purchaseDatas.value + 1;

            }
            ItemsPanel.transform.GetChild(i).GetChild(0).GetChild(0).name = itemsList[rnd[i]].objname;//オブジェクト名変更
            ItemsPanel.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = itemsList[rnd[i]].name;//アイテム名変更
            ItemsPanel.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{price1 * price2}";//アイテム値段変更
            ItemsPanel.transform.GetChild(i).name = $"{i}";
            purchaseTmp_s2 = $"{i}";
            //色変更
            ColorChange(ItemsPanel.transform.GetChild(i).GetChild(0).GetChild(0).gameObject, color_i);
        }

    }
    public void shopPurchaseAdd()
    {
        var itemDatas = itemsList.Find(x => x.objname == purchaseTmp_s);
        var purchaseDatas = purchaseList.Find(x => x.objname == purchaseTmp_s);

        if (purchaseDatas != null)
        {
            if (purchaseDatas.value < 4)
                purchaseList.Find(x => x.objname == purchaseTmp_s).value += 1;
        }
        //初回購入
        else
            purchaseList.Add(new Data_Items()
            {
                No = itemDatas.No,
                name = itemDatas.name,
                objname = itemDatas.objname,
                price = itemDatas.price,
                value = 1,
                custom = false,
                icon = null,
                image = null,
            });

        ES3.Save(playerFlagKey[2], purchaseList);//購入済みリストセーブ
    }
    public void shopPurchaseAddStart(string objname)//スタート時のみ
    {
        var itemDatas = itemsList.Find(x => x.objname == objname);
        var purchaseDatas = purchaseList.Find(x => x.objname == objname);
        if (purchaseDatas != null)
        {
            return;
        }
        if(objname == "B_Divide" || objname == "B_Minus" || objname == "B_Multiplied" || objname == "B_Plus")
        {
            itemDatas.value = 3;
        }
        purchaseList.Add(new Data_Items()
        {
            No = itemDatas.No,
            name = itemDatas.name,
            objname = itemDatas.objname,
            price = itemDatas.price,
            value = itemDatas.value,
            custom = true,
            icon = null,
            image = null,
        });
    }
    //====================================================================================================
    //システム処理
    //====================================================================================================
    //スコア計算関数
    public void scoreCount(int flag)
    {
        int bPoint = 0;
        int fSPoint = 0;
        int total = 0;
        int add_s = 0;//スコア用
        int add_c = 0;//コイン用
        int enemy = enemyList[0];
        int mag = 1;//倍率
        string eva = evaluation[0];//評価
        if (flag == 0)
        {
            //スコア
            var max = Mathf.Max(scoreList[0][0], scoreList[0][1]);// 最大値を求める
            var min = Mathf.Min(scoreList[0][0], scoreList[0][1]);// 最小値を求める
            var answer = answerList[0];
            bPoint = basePoint_L[0];
            fSPoint = formulaSymbolPoint_L[0];

            //レベルにより最大数更新
            if (playerData[0] >= 20)//プレイヤーレベル20以上で増えていく
            {
                mag = playerData[0] / 10;

            }

            //足し算
            if (scoreList[0][2] == 0)
            {
                int i = max - min;
                if (i < 2)
                {
                    add_s += 10;
                    eva = evaluation[4];//エクセレント
                }
                else if (i >= 2 && i < 10)
                {
                    add_s += 8;
                    eva = evaluation[3];//グッド
                }
                else if (i >= 10 && i < 100)
                {
                    add_s += 6;
                    eva = evaluation[2];//ナイス
                }
                else if (i >= 100)
                {
                    add_s += 4;
                    eva = evaluation[1];//プア
                }


                //加減点
                if (min < 5)
                {
                    //減点
                    add_s -= 5;
                }

                if (min > 10)
                {
                    //追加点
                    add_s += 5;
                }
            }
            //引き算
            else if (scoreList[0][2] == 1)//-
            {
                if (min > answer)
                {
                    //最高得点
                    add_s += 10;
                    eva = evaluation[4];//エクセレント
                }
                if (min < 10)
                {
                    //減点
                    add_s -= 5;
                    eva = evaluation[1];//プア
                }
            }
            //掛け算
            else if (scoreList[0][2] == 2)//*
            {
                int i = max - min;
                if (min < 2)//1で掛けた場合減点
                {
                    //素数が含まれる場合は減点しない代わりにボーナス
                    for (int i2 = 0; i2 < primeNumber.Length; i2++)
                    {
                        if (answer == primeNumber[i2])
                        {
                            //素数ボーナス
                            add_s += i2;
                            break;
                        }
                    }
                    //減点
                    add_s -= 9;
                    eva = evaluation[1];//プア
                }
                else if (min > 1 && min <= 3)//2で掛けた場合
                {
                    add_s += min;
                    eva = evaluation[2];//ナイス
                }
                else if (min > 2 && min <= 10)//3〜10で掛けた場合
                {
                    add_s += min;
                    eva = evaluation[3];//グッド
                }
                else if (min > 10)//11〜で掛けた場合
                {
                    add_s += min;
                    eva = evaluation[4];//エクセレント
                }
                //ククボーナス
                if (i == 0)
                {
                    add_s += min;
                }
            }
            //割り算
            else if (scoreList[0][2] == 3)// /
            {
                if (min == 1 || min == 10 || min == 100)//1,10,100で割った場合減点
                {
                    //素数が含まれる場合は減点しない
                    for (int i2 = 0; i2 < primeNumber.Length; i2++)
                    {
                        if (answer == primeNumber[i2])
                        {
                            //素数ボーナス
                            add_s += i2;
                            break;
                        }
                    }
                    //減点
                    add_s = 1;
                    eva = evaluation[1];//プア
                }
                else if (min > 1 && min <= 3)//2で割った場合
                {
                    add_s += min;
                    eva = evaluation[2];//ナイス
                }
                else if (min > 2 && min <= 9)//3〜9で割った場合
                {
                    add_s += 10 + min;
                    eva = evaluation[3];//グッド
                }
                else if (min > 10)//11〜で割った場合
                {
                    add_s += 20 + min;
                    eva = evaluation[4];//エクセレント
                }
            }

            //スコア計算
            playerData[3] += (scoreAdd + add_s + bPoint) * fSPoint * mag;
            scoreClear += (scoreAdd + add_s + bPoint) * fSPoint * mag;
            total = (scoreAdd + add_s + bPoint) * fSPoint * mag;
            playerDataText[3].text = $"SCORE:{playerData[3] }";
            evaluationText.text = $"{eva}({scoreAdd + add_s})";//評価

            //ハイスコア以上なら更新
            if (playerData[3] > highScore)
            {
                highScoreText.text = $"HIGHSCORE:{playerData[3]}";
            }
            //コイン加算
            if (enemy * 2 > enemyLvMax)
            {
                add_c += 2;
            }
            if (enemy > 100)
            {
                add_c += playerData[0] / 10;
            }

            //コイン計算
            playerData[2] += coinAdd + add_c;//コイン追加
            coinClear += coinAdd + add_c;//クリア表示用コイン追加
            playerDataText[2].text = $"{playerData[2]}";//テキスト変更


        }
        else if (flag == 2)
        {
            aanswerCount.Add("-");
            formulaSymbolPointCount.Add(0);
            formulaSymbolCount.Add(" ");
            basePointCount.Add(0);
        }
        if (flag == 1 || flag == 2)
        {
            mag = 0;
        }
    
        //スコア表示用
        enemyCount.Add(enemy);
        evaCount.Add(eva);
        magCount.Add(mag);
        addCount.Add(add_s);
        totalCount.Add(total);
    }
    public void GameClearCount()
    {
        defeat += 1;
        if (defeat < enemyNum)
        {
            return;
        }
        //ゲームオーバー処理へ
        if (playerData[4] == 0)
        {
            return;
        }
        if (playerData[5] == 0)
        {
            return;
        }
        //テキスト表示
        StartCoroutine(titleManager.ShowCoroutine());
        //アニメーション
        int rnd = Random.Range(1, 3); // ※ 1～99の範囲でランダムな整数値が返る
        if (rnd == 1) {
            characterStagePre.GetComponent<Animator>().SetBool("Victory01_B", true);//モーション変更
        }
        else
        {
            characterStagePre.GetComponent<Animator>().SetBool("Victory02_B", true);//モーション変更
        }
        //レベルアップ
        playerData[0] += 1;
        playerDataText[0].text = $"{playerData[0]} ";
        //セーブ
        ES3.Save<int>(playerDataKey[0], playerData[0]);//レベルセーブ
        ES3.Save<int>(playerDataKey[2], playerData[2]);//コインセーブ
        ES3.Save<int>(playerDataKey[3], playerData[3]);//スコアセーブ
        ES3.Save<int>("HIGHSCORE", playerData[3]);//ハイスコアセーブ

        //ショップ更新
        shopUpdate();
        //パネル表示
        StartCoroutine(GameClearPanel());
    }
    IEnumerator GameClearPanel()
    {
        yield return new WaitForSeconds(2);
        //表示
        titleManager.dotween_scale(titleManager.AllPanel[2]);
        titleManager.AllPanel[2].transform.Find("MainPanel/ScrollView/Viewport/Content/Lv/Text").GetComponent<TextMeshProUGUI>().text = $"{playerData[0] - 1} → {playerData[0]}";
        titleManager.AllPanel[2].transform.Find("MainPanel/ScrollView/Viewport/Content/Score/Text").GetComponent<TextMeshProUGUI>().text = $"+{scoreClear}";
        titleManager.AllPanel[2].transform.Find("MainPanel/ScrollView/Viewport/Content/Coin/Text").GetComponent<TextMeshProUGUI>().text = $"+{coinClear}";
        evaluationText.text = "";
        //テキスト非表示
        titleManager.successText.gameObject.SetActive(false);
        //モーション戻す
        characterStagePre.GetComponent<Animator>().SetBool("Victory01_B", false);//モーション変更
        characterStagePre.GetComponent<Animator>().SetBool("Victory02_B", false);//モーション変更

        yield return new WaitForSeconds(0.2f);
        //スコアの内訳
        for (int i = 0; i < enemyCount.Count; i++)
        {
            GameObject Obj;
            Obj = (GameObject)Instantiate(ScoreCountPre, ScoreCountPre.transform.position, Quaternion.identity);
            Obj.transform.SetParent(ScoreCount_C.transform, false);
            //敵の数字
            Obj.transform.Find("Panel0/Text").GetComponent<TextMeshProUGUI>().text = $"{enemyCount[i]}";
            //評価
            Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"{evaCount[i]}";
            //式
            Obj.transform.Find("Panel1/Text").GetComponent<TextMeshProUGUI>().text = $"{aanswerCount[i]}";
            //基礎
            Obj.transform.Find("Panel2/Panel0/Text").GetComponent<TextMeshProUGUI>().text = $"{scoreAdd}";
            //式
            Obj.transform.Find("Panel2/Panel1/Text").GetComponent<TextMeshProUGUI>().text = $"{addCount[i]}";
            //数
            Obj.transform.Find("Panel2/Panel2/Text").GetComponent<TextMeshProUGUI>().text = $"{basePointCount[i]}";
            //記号
            Obj.transform.Find("Panel2/Panel3/Text").GetComponent<TextMeshProUGUI>().text = $"{formulaSymbolPointCount[i]}({formulaSymbolCount[i]})";
            //倍率
            Obj.transform.Find("Panel2/Panel4/Text").GetComponent<TextMeshProUGUI>().text = $"{magCount[i]}";
            //合計
            Obj.transform.Find("Panel2/Panel5/Text").GetComponent<TextMeshProUGUI>().text = $"{totalCount[i]}";
            //動き
            titleManager.dotween_scale(Obj);
            yield return new WaitForSeconds(0.05f);
        }

        defeat = 0;//クリア
        scoreClear = 0;//クリア
        coinClear = 0;//クリア
    }
    public void GameOverCount()
    {
        //ゲームオーバー処理
        int count = 0;
        if (playerData[4] > 0)
        {
            count++;
        }
        if (playerData[5] > 0)
        {
            count++;
        }
        if (count == 2)
        {
            return;
        }
        characterStagePre.GetComponent<Animator>().SetBool("Die02_B", true);//モーション変更
        //データ削除
        for (int i = 0; i < playerData.Length; i++)
        {
            if (playerDataKey[i] != "STAR")
            {
                ES3.DeleteKey(playerDataKey[i]);
            }
        }
        //フラグ削除
        ES3.DeleteKey(playerFlagKey[0]);
        ES3.DeleteKey(playerFlagKey[1]);
        ES3.DeleteKey(playerFlagKey[2]);
        ES3.DeleteKey(playerShopKey[0]);
        ES3.DeleteKey(playerShopKey[1]);

        //デス数カウント
        int deathCount = ES3.Load<int>("DEATH", defaultValue: 0);
        deathCount++;
        ES3.Save<int>("DEATH", deathCount);//デス数セーブ

        foreach (Transform top in EnemyArea.transform)
        {
            Destroy(top.gameObject, 0.1f);
        }
        foreach (Transform under in GameUnder.transform)
        {
            Destroy(under.gameObject, 0.1f);
        }
        //パネル表示
        StartCoroutine(GameOverPanel());

    }
    IEnumerator GameOverPanel()
    {
        titleManager.PlayBgm(null);//BGM一時停止
        yield return new WaitForSeconds(2);
        titleManager.seSource.PlayOneShot(titleManager.seClip[14]);//ゲームオーバー音
        titleManager.AllPanel[1].gameObject.SetActive(true);//ゲームオーバーパネル表示
        titleManager.AllPanel[1].GetComponent<CanvasGroup>().DOFade(1.0f, 0.5f).SetEase(Ease.InQuart);
        titleManager.AllPanel[1].transform.Find("MainPanel/Text1").DOScale(new Vector3(1.0f, 1.0f, 1.0f),1.5f);//演出時間
        yield return new WaitForSeconds(1.5f);
        titleManager.GameOverBackButton.GetComponent<CanvasGroup>().DOFade(1.0f, 0.3f).SetEase(Ease.InQuart);
        characterStagePre.GetComponent<Animator>().SetBool("Die02_B", false);//モーション変更
    }
    public void comboFalse()
    {
        combo = 0;
        comboText.text = $"";
        evaluationText.text = evaluation[0];
        ComboPanel.SetActive(false);
    }
    public void comboTrue()
    {
        ComboPanel.SetActive(true);
        ComboPanel.transform.localScale = Vector3.zero;
        ComboPanel.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
        combo += 1;
        comboText.text = $"{combo}";
    }
}
