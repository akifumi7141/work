using System.Collections;
using System.Collections.Generic;
//using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using System.Linq;
using TMPro;

public class TitleButton : MonoBehaviour
{

    private GameObject Script;
    TitleManager titleManager;
    BattleManager battleManager;
    languageText languagetext;

    public string[] symbol = { "+", "-", "*", "/" };

    //言語判定
    public int languageData;

    void Awake()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        battleManager = Script.GetComponent<BattleManager>();
        languagetext = Script.GetComponent<languageText>();
        //言語格納
        languageData = ES3.Load<int>("LANGUAGE", defaultValue: 0);
    }
    //====================================================================================================
    #region //各ボタン処理
    //====================================================================================================
    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "ConfigButton"://設定ボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                dotween_scale(titleManager.AllPanel[0]);
                break;
            case "ConfigBackButton"://設定戻るボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                dotween_fade(titleManager.AllPanel[0]);
                Button_Emphasis1(this.gameObject);//ボタン強調表示
                PlayerPrefs.SetFloat(TitleManager.SE, TitleManager.fSE);
                PlayerPrefs.SetFloat(TitleManager.Music, TitleManager.fMusic);
                PlayerPrefs.Save();
                break;
            case "GameOverBackButton"://ゲームオーバー戻るボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                StartCoroutine(GameOverPanelEnd());
                break;
            case "GameClearNextButton"://ゲームクリア次へボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//yes

                //初期化
                for (int i = 0; i < battleManager.enemyCount.Count; i++)
                {
                    Transform children = battleManager.ScoreCount_C.transform.GetComponentInChildren<Transform>();
                    //子要素がいなければ終了
                    if (children.childCount != 0)
                    {
                        Destroy(battleManager.ScoreCount_C.transform.GetChild(i).gameObject);
                    }
                }
                battleManager.enemyCount = new List<int>();
                battleManager.evaCount = new List<string>();
                battleManager.aanswerCount = new List<string>();
                battleManager.addCount = new List<int>();
                battleManager.basePointCount = new List<int>();
                battleManager.formulaSymbolPointCount = new List<int>();
                battleManager.formulaSymbolCount = new List<string>();
                battleManager.magCount = new List<int>();
                battleManager.totalCount = new List<int>();

                titleManager.TogglePanel.SetActive(true);
                titleManager.TogglePanel.transform.GetChild(2).GetComponent<Toggle>().isOn = true;
                titleManager.AllPanel[2].SetActive(false);
                break;
            case "BattleStart"://ゲームスタート
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                titleManager.TogglePanel.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
                
                break;
            case "SETextButton"://SEテストボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                break;
            case "B_DataDeletion"://データ削除ボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                                                                          //データ削除
                for (int i = 0; i < battleManager.playerData.Length; i++)
                {
                    if (battleManager.playerDataKey[i] != "STAR")
                    {
                        ES3.DeleteKey(battleManager.playerDataKey[i]);
                    }
                }
                //フラグ削除
                ES3.DeleteKey(battleManager.playerFlagKey[0]);
                ES3.DeleteKey(battleManager.playerFlagKey[1]);
                ES3.DeleteKey(battleManager.playerFlagKey[2]);
                ES3.DeleteKey(battleManager.playerShopKey[0]);
                ES3.DeleteKey(battleManager.playerShopKey[1]);
                SceneNavigator.Instance.Change("02_Title", 0.5f);
                break;
            case "B_DataClear"://データクリアボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                //データ削除
                ES3.DeleteFile("SaveFile.es3");
                SceneNavigator.Instance.Change("02_Title", 0.5f);
                break;
            //サイドボタン
            case "B_costume"://コスチュームボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                dotween_scale(titleManager.CostumePanel);
                break;
            case "B_CostumeGet"://コスチュームGetボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[13]);//yes
                //購入処理
                battleManager.playerData[1] -= titleManager.L_basket1.Count * 500;
                battleManager.playerDataText[1].text = $"{battleManager.playerData[1]}";//テキスト変更
                //購入済み処理
                for (int i = 0; i < titleManager.L_basket1.Count; i++)
                {
                    battleManager.costumeGet[titleManager.L_basket1[i]][titleManager.L_basket2[i]] = true;
                }
                //購入済み表示変更
                for (int j = 0; j < 4; j++)
                {
                    for (int j2 = 0; j2 < battleManager.costumeGet[j].Length; j2++)
                    {
                        if (battleManager.costumeGet[j][j2] == true && j2 != 0)
                        {
                            titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(j).GetChild(j2).Find("Panel/Panel2/Panel").gameObject.SetActive(true);
                        }
                    }
                }
                //セーブ
                ES3.Save<int>(battleManager.playerDataKey[1], battleManager.playerData[1]);//スターセーブ
                ES3.Save(battleManager.playerFlagKey[3], battleManager.characterLoop);//キャラクターセーブ
                ES3.Save(battleManager.playerFlagKey[5], battleManager.costumeGet);
                break;
            case "B_CostumeSave"://コスチュームセーブボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                //ボタン処理
                titleManager.B_CostumeSave.GetComponent<Button>().interactable = false;
                titleManager.B_CostumeGet.GetComponent<Button>().interactable = false;
                //セーブ
                ES3.Save(battleManager.playerFlagKey[3], battleManager.characterLoop);//キャラクターコスチュームセーブ
                break;
            case "B_CostumeBack"://コスチュームバックボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                //変更がない場合元に戻す。未購入のものが選択されている場合元に戻す。
                battleManager.characterLoop = ES3.Load<int[]>(battleManager.playerFlagKey[3], defaultValue: new int[8] { 0, 0, 0, 0, 99, 99, 99, 99 });//キャラクター反映
                battleManager.characterUpdate(battleManager.characterStagePre);
                battleManager.characterUpdate(battleManager.characterHomePre);
                for (int i3 = 0; i3 < 4; i3++)
                {
                    titleManager.TogglePanel_Costume.transform.GetChild(i3).GetComponent<Toggle>().isOn = true;
                    //装着中をトグルon
                    if (battleManager.characterLoop[i3 + 4] == 99)
                    {
                        
                        titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(i3).GetChild(0).GetComponent<Toggle>().isOn = true;
                    }
                    else
                    {
                        titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(i3).GetChild(battleManager.characterLoop[i3 + 4] + 1).GetComponent<Toggle>().isOn = true;
                    }

                }
                //コンテンツ
                titleManager.TogglePanel_Costume.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
                //ボタン処理
                titleManager.B_CostumeSave.GetComponent<Button>().interactable = false;
                titleManager.B_CostumeGet.GetComponent<Button>().interactable = false;
                //パネル処理
                dotween_fade(titleManager.CostumePanel);
                Button_Emphasis1(this.gameObject);//ボタン強調表示
                break;
            case "B_ranking"://ランキングボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                break;
            case "B_LoginBonus"://ログインボーナスボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                dotween_scale(titleManager.LoginBonusPanel);
                break;
            case "B_LoginBonusOK"://ログインボーナスOKボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[13]);//ログボ獲得
                //スター取得
                battleManager.playerData[1] += 100;//変数格納
                battleManager.playerDataText[1].text = $"{battleManager.playerData[1]}";//テキスト変更
                ES3.Save<int>(battleManager.playerDataKey[1], battleManager.playerData[1]);//データセーブ
                //ログインボーナス現在の状態変更
                titleManager.B_LoginBonus.GetComponent<Button>().interactable = false;
                ES3.Save<bool>(battleManager.playerFlagKey[4], false);//データセーブ
                //パネル処理
                dotween_fade(titleManager.LoginBonusPanel);
                Button_Emphasis1(this.gameObject);//ボタン強調表示
                break;
            case "Twitter"://Twitterボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                Application.OpenURL("https://twitter.com/rasisProject");
                break;
            case "YouTube"://YouTubeボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                Application.OpenURL("https://www.youtube.com/channel/UCWgdHfbDr2nFRHNolSRJroQ/");
                break;
            case "hatena"://はてなブログボタン
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                break;
            case "PrivacyPolicyButton"://プライバシーポリシー
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                Application.OpenURL("https://akifumi7141.hatenablog.com/entry/2023/11/28/023009/");
                break;
            default:
                break;
        }
    }
    #endregion
    //====================================================================================================
    //ゲームオーバー
    //====================================================================================================
    IEnumerator GameOverPanelEnd()
    {
        yield return new WaitForSeconds(0.2f);

        titleManager.AllPanel[1].transform.Find("MainPanel").GetComponent<CanvasGroup>().DOFade(0.0f, 0.5f).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(0.6f);
        SceneNavigator.Instance.Change("02_Title", 0.5f);
    }
    public void starTest()
    {
        //スター取得
        battleManager.playerData[1] += 500;//変数格納
        battleManager.playerDataText[1].text = $"{battleManager.playerData[1]}";//テキスト変更
        ES3.Save<int>(battleManager.playerDataKey[1], battleManager.playerData[1]);//データセーブ
    }
    public void coinTest()
    {
        //コイン取得
        battleManager.playerData[2] += 500;//変数格納
        battleManager.playerDataText[2].text = $"{battleManager.playerData[2]}";//テキスト変更
        ES3.Save<int>(battleManager.playerDataKey[2], battleManager.playerData[2]);//データセーブ
    }
    //====================================================================================================
    #region //コスチューム選択用
    //====================================================================================================
    public void CostumeToggle(int i)
    {
        Toggle toggle = this.GetComponent<Toggle>();
        if (toggle.isOn)
        {
            titleManager.ScrollView_Costume.GetComponent<ScrollRect>().content = titleManager.Content_Costume[i];//スクロール変更用
            for (int i2 = 0; i2 < 4; i2++)
            {
                if (i == i2)
                    titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(i2).gameObject.SetActive(true);
                else
                    titleManager.ScrollView_Costume.transform.Find("Viewport").GetChild(i2).gameObject.SetActive(false);
            }
        }
    }
    #endregion
    //====================================================================================================
    #region //コスチューム選択用
    //====================================================================================================
    public void SV_CostumeToggle(int i)
    {
        if (this.transform.parent.gameObject.name == "C_Bodyparts")
        {
            battleManager.characterLoop[4] = i;
        }
        else if (this.transform.parent.gameObject.name == "C_Gloves")
        {
            battleManager.characterLoop[5] = i;
        }
        else if (this.transform.parent.gameObject.name == "C_Headparts")
        {
            battleManager.characterLoop[6] = i;
        }
        else if (this.transform.parent.gameObject.name == "C_Tails")
        {
            battleManager.characterLoop[7] = i;
        }
        //買い物かご
        titleManager.L_basket1 = new List<int>();
        titleManager.L_basket2 = new List<int>();
        for (int i2 = 0; i2 < 4; i2++)
        {
            //未購入の場合リスト追加
            if (battleManager.characterLoop[i2 + 4] != 99) {
                if (battleManager.costumeGet[i2][battleManager.characterLoop[i2 + 4]+1] == false)
                {
                    titleManager.L_basket1.Add(i2);
                    titleManager.L_basket2.Add(battleManager.characterLoop[i2 + 4]+1);
                    
                }
            }
        }
        //購入ボタン判定
        if (titleManager.L_basket1.Count != 0 && battleManager.playerData[1] >= titleManager.L_basket1.Count * 500)
        {
            titleManager.B_CostumeGet.GetComponent<Button>().interactable = true;
        }
        else
        {
            titleManager.B_CostumeGet.GetComponent<Button>().interactable = false;
        }
        //セーブボタン判定
        int counnt = 0;
        int[] j = ES3.Load<int[]>(battleManager.playerFlagKey[3], defaultValue: new int[8] { 0, 0, 0, 0, 99, 99, 99, 99 });//今の情報を反映
        for (int i3 = 0;i3 < 4; i3++)
        {
            if (battleManager.characterLoop[i3 + 4] != j[i3 + 4] && titleManager.L_basket1.Count == 0)
            {

                counnt += 1;
            }
        }
        if (counnt > 0)
        {
            titleManager.B_CostumeSave.GetComponent<Button>().interactable = true;
        }
        else
        {
            titleManager.B_CostumeSave.GetComponent<Button>().interactable = false;
        }
        //必要スター表示
        titleManager.B_CostumeGet.transform.Find("Panel/Text").GetComponent<TextMeshProUGUI>().text = $"{titleManager.L_basket1.Count * 500}";
        //スキン変更
        battleManager.characterUpdate(battleManager.characterStagePre);
        battleManager.characterUpdate(battleManager.characterHomePre);

    }
    #endregion
    //====================================================================================================
    #region //キャラクター選択用
    //====================================================================================================
    public void ButtonClickStart()
    {
        switch (transform.name)
        {
            //ボディ
            case "B_Bodies_L"://左ボタン
                battleManager.characterLoop[0] = (battleManager.characterLoop[0] + 5) % 6;
                battleManager.characterLoopText[0].text = $"{battleManager.characterLoop[0] + 1}";
                for (int i =0;i<6;i++)
                {
                    if (i == battleManager.characterLoop[0])
                    {
                        battleManager.characterPre.transform.Find("Bodies").GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        battleManager.characterPre.transform.Find("Bodies").GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
            case "B_Bodies_R"://右ボタン
                battleManager.characterLoop[0] = (battleManager.characterLoop[0] + 1) % 6;
                battleManager.characterLoopText[0].text = $"{battleManager.characterLoop[0] + 1}";
                for (int i = 0; i < 6; i++)
                {
                    if (i == battleManager.characterLoop[0])
                    {
                        battleManager.characterPre.transform.Find("Bodies").GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        battleManager.characterPre.transform.Find("Bodies").GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
                //目
            case "B_Eyes_L"://左ボタン
                battleManager.characterLoop[1] = (battleManager.characterLoop[1] + 10) % 11;
                battleManager.characterLoopText[1].text = $"{battleManager.characterLoop[1] + 1}";
                for (int i = 0; i < 11; i++)
                {
                    if (i == battleManager.characterLoop[1])
                    {
                        battleManager.characterPre.transform.Find("Eyes").GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        battleManager.characterPre.transform.Find("Eyes").GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
            case "B_Eyes_R"://右ボタン
                battleManager.characterLoop[1] = (battleManager.characterLoop[1] + 1) % 11;
                battleManager.characterLoopText[1].text = $"{battleManager.characterLoop[1] + 1}";
                for (int i = 0; i < 11; i++)
                {
                    if (i == battleManager.characterLoop[1])
                    {
                        battleManager.characterPre.transform.Find("Eyes").GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        battleManager.characterPre.transform.Find("Eyes").GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
                //口鼻
            case "B_MN_L"://左ボタン
                battleManager.characterLoop[2] = (battleManager.characterLoop[2] + 14) % 15;
                battleManager.characterLoopText[2].text = $"{battleManager.characterLoop[2] + 1}";
                for (int i = 0; i < 15; i++)
                {
                    if (i == battleManager.characterLoop[2])
                    {
                        battleManager.characterPre.transform.Find("MouthandNoses").GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        battleManager.characterPre.transform.Find("MouthandNoses").GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
            case "B_MN_R"://右ボタン
                battleManager.characterLoop[2] = (battleManager.characterLoop[2] + 1) % 15;
                battleManager.characterLoopText[2].text = $"{battleManager.characterLoop[2] + 1}";
                for (int i = 0; i < 15; i++)
                {
                    if (i == battleManager.characterLoop[2])
                    {
                        battleManager.characterPre.transform.Find("MouthandNoses").GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        battleManager.characterPre.transform.Find("MouthandNoses").GetChild(i).gameObject.SetActive(false);
                    }
                }
                break;
            default:
                break;
        }

        titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
        //battleManager.topLvImage.GetComponent<Image>().sprite = battleManager.characterList[i].icon;//アイコン画像変更
    }
    //タイプトグル変更
    public void toggleClickStart(int i)
    {
        if (this.GetComponent<Toggle>().isOn == true)
        {
            battleManager.keyListCustom[1] = battleManager.KeyListStart[i].name;//保存用文字配列に格納
            battleManager.characterLoop[3] = i;//ループ変数に格納
            //キー配置
            if (battleManager.KeyBase.transform.GetChild(1).childCount != 0)
            {
                Destroy(battleManager.KeyBase.transform.GetChild(1).GetChild(0).gameObject);
            }
            GameObject Obj;
            Obj = (GameObject)Instantiate(battleManager.KeyListStart[i], battleManager.KeyListStart[i].transform.position, Quaternion.identity);
            Obj.transform.SetParent(battleManager.KeyBase.transform.GetChild(1).transform, false);
        }
    }
    //キャラクター選択決定
    public void ButtonClickStartOK()
    {
        StartCoroutine(ButtonClickStartOK_A());
    }
    IEnumerator ButtonClickStartOK_A()
    {
        battleManager.characterPre.GetComponent<Animator>().SetTrigger("Jump_T");//モーション変更
        yield return new WaitForSeconds(0.2f);
        titleManager.Cube.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Loading_On(0);
        battleManager.characterHomePre.transform.position = new Vector3(0.0f, 40.0f, 0.0f);
        battleManager.characterHomePre.transform.DOLocalMoveY(0, 2).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1);
        
        //スタートホームパネル非表示
        battleManager.SelectPanel.SetActive(false);//スタートパネル表示

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
        battleManager.canvas.worldCamera = titleManager.Camera[4].GetComponent<Camera>();

        //画像変更
        //titleManager.GamePanel[1].transform.Find("Under/Player").GetComponent<Image>().sprite = battleManager.characterList[battleManager.characterLoop].image;//バトルパネルのキャラクター画像変更
        battleManager.characterUpdate(battleManager.characterStagePre);
        battleManager.characterUpdate(battleManager.characterHomePre);

        //購入済みリストにも格納
        foreach (var key in battleManager.keyListCustom)
        {
            battleManager.shopPurchaseAddStart(key);
        }
        //キー情報更新
        battleManager.keyUpdate();
        //データセーブ
        ES3.Save<bool>(battleManager.playerFlagKey[0], true);//キャラクターフラグセーブ
        ES3.Save(battleManager.playerFlagKey[1], battleManager.keyListCustom);//現在のキーを保存
        ES3.Save(battleManager.playerFlagKey[2], battleManager.purchaseList);//購入済みリストセーブ
        ES3.Save(battleManager.playerFlagKey[3], battleManager.characterLoop);//キャラクターセーブ
        ES3.Save(battleManager.playerFlagKey[5], battleManager.costumeGet);//コスチュームセーブ
        yield return new WaitForSeconds(3);
        battleManager.characterPre.GetComponent<Animator>().SetTrigger("Die01Recover_T");//モーション変更
    }
    //====================================================================================================
    //ローディング
    //====================================================================================================
    public void Loading_On(int count)
    {
        titleManager.PlayBgm(null);//BGM一時停止
        titleManager.LoadingPanel.SetActive(true);//ローディング表示
        battleManager.keyMovementOff(count);//編集機能ON/Off
        titleManager.LoadingPanel.transform.GetChild(0).GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
        titleManager.LoadingPanel.GetComponent<Image>().fillAmount = 0;
        titleManager.LoadingPanel.transform.GetChild(0).GetComponent<Image>().DOFillAmount(1, 0.5f).OnStepComplete(() => Loading_Off(count));
    }
    public void Loading_Off(int count)
    {
        if (titleManager.TogglePanel.transform.GetChild(count).GetComponent<Toggle>().isOn == true)
        {
            //カメラ
            if (count == 0)//ホーム
            {
                titleManager.TogglePanel.SetActive(true);
                titleManager.Camera[1].SetActive(false);
                titleManager.Camera[2].SetActive(false);
                titleManager.Camera[3].SetActive(false);
                titleManager.Camera[4].SetActive(true);
                battleManager.canvas.worldCamera = titleManager.Camera[4].GetComponent<Camera>();
            }
            else if (count == 2)//ショップ
            {
                titleManager.TogglePanel.SetActive(true);
                titleManager.Camera[1].SetActive(false);
                titleManager.Camera[2].SetActive(false);
                titleManager.Camera[3].SetActive(true);
                titleManager.Camera[4].SetActive(false);
                battleManager.canvas.worldCamera = titleManager.Camera[3].GetComponent<Camera>();
                titleManager.B_ShopChange_R.SetActive(true);
                titleManager.B_ShopChange_L.SetActive(false);
                battleManager.ItemsPanel.SetActive(true);
                battleManager.ItemsPanel2.SetActive(false);
                titleManager.Camera[3].transform.DOLocalMoveX(0, 0.3f).SetEase(Ease.Linear);
                if (languageData == 0)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 いつもおつかれさま 」";
                if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Thanks for everything";
            }
            else if (count == 1)//バトル
            {
                titleManager.TogglePanel.SetActive(false);
                titleManager.Camera[1].SetActive(true);
                titleManager.Camera[2].SetActive(false);
                titleManager.Camera[3].SetActive(false);
                titleManager.Camera[4].SetActive(false);
                battleManager.canvas.worldCamera = titleManager.Camera[1].GetComponent<Camera>();
                battleManager.enemyRandom();//ゲームスタート

            }
            else if (count == 4)//デッキ
            {
                titleManager.TogglePanel.SetActive(true);
                titleManager.Camera[1].SetActive(true);
                titleManager.Camera[2].SetActive(false);
                titleManager.Camera[3].SetActive(false);
                titleManager.Camera[4].SetActive(false);
                battleManager.canvas.worldCamera = titleManager.Camera[1].GetComponent<Camera>();
            }
        }
            //トグル表示
            for (int i = 0; i < titleManager.GamePanel.Length; i++)
            {
                if (count != i)
                {
                    titleManager.GamePanel[i].SetActive(false);
                    titleManager.TogglePanel.transform.GetChild(i).GetComponent<Toggle>().interactable = true;
                }
                else
                {
                    titleManager.GamePanel[i].SetActive(true);
                    titleManager.TogglePanel.transform.GetChild(i).GetComponent<Toggle>().interactable = false;
                }
            }
        
        titleManager.LoadingPanel.transform.GetChild(0).GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
        titleManager.PlayBgm(titleManager.clipBGM[count]);
        titleManager.LoadingPanel.transform.GetChild(0).GetComponent<Image>().DOFillAmount(0, 0.5f).SetDelay(1).OnStepComplete(() => titleManager.LoadingPanel.SetActive(false));
    }
    public void purchaseListUpdateButton()//キー情報更新
    {
        titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
        battleManager.purchaseListUpdate();
    }
    #endregion
    //====================================================================================================
    //ショップボタン処理
    //====================================================================================================
    public void shopUpdateButton()//更新
    {
        if (battleManager.playerData[2] > 0)
        {
            titleManager.seSource.PlayOneShot(titleManager.seClip[11]);//コイン更新音
            battleManager.shopUpdate();
            battleManager.consumption = 5;//消費数
            battleManager.playerData[2] -= battleManager.consumption;//コイン消費
            battleManager.playerDataText[2].text = $"{battleManager.playerData[2]}";//テキスト変更
            titleManager.TextBox.SetActive(false);
            ES3.Save<int>(battleManager.playerDataKey[2], battleManager.playerData[2]);//コインセーブ
        }
        else
        {
            titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
        }
    }
    public void shopPurchaseButton()//アイテム選択
    {
        titleManager.seSource.PlayOneShot(titleManager.seClip[2]);//yes
        var itemDatas = battleManager.itemsList.Find(x => x.objname == this.name);//アイテムリストから参照
        var purchaseDatas = battleManager.purchaseList.Find(x => x.objname == this.name);//購入リストから参照
        battleManager.purchaseTmp_s = itemDatas.objname;//判定用一時格納
        battleManager.purchaseTmp_s2 = this.transform.parent.transform.parent.name;
        battleManager.purchaseTmp_i2 = 0;

        titleManager.TextBox.SetActive(true);//テキストボックス表示
        if (itemDatas.No >= 11) {
            if (purchaseDatas == null)
            {
                     if (languageData == 0)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 {itemDatas.name} 」 が使用可能になるネ";
                else if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 {itemDatas.name} 」 will be available";
            }
            else
            {
                if (languageData == 0)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 {itemDatas.name} 」 が強化され 「 スコア 」 が高くなるネ";
                else if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"The {itemDatas.name} is enhanced and the score is higher";
            }
        }
        else
        {
            if (languageData == 0)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 {itemDatas.name} 」 が強化され 「 スコア 」 が高くなるネ";
            else if (languageData == 1)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"The {itemDatas.name} is enhanced and the score is higher";
        }
        titleManager.TextBox.transform.Find("PurchaseButtonPanel").gameObject.SetActive(true);
    }
    public void shopPurchaseButton2(int i)//アイテム選択2
    {
        titleManager.seSource.PlayOneShot(titleManager.seClip[2]);//yes
        battleManager.purchaseTmp_i = i;
        battleManager.purchaseTmp_i2 = 1;
        titleManager.TextBox.SetActive(true);//テキストボックス表示
        if (i == 0)
        {
            if (languageData == 0)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 ハート」 が 「 1回復 」 するゾ";
            else if (languageData == 1)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Hearts recover by one";
        }
        else if (i == 1)
        {
            if (languageData == 0)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 シールド」 が 「 全回復 」 するゾ";
            else if (languageData == 1)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Shield is fully restored";
        }
        titleManager.TextBox.transform.Find("PurchaseButtonPanel").gameObject.SetActive(true);
        
    }
    public void shopPurchaseButtonYes()//購入
    {
        if (battleManager.purchaseTmp_i2 == 0)
        {
            var itemDatas = battleManager.itemsList.Find(x => x.objname == battleManager.purchaseTmp_s);
            var purchaseDatas = battleManager.purchaseList.Find(x => x.objname == battleManager.purchaseTmp_s);
            int price1 = 1;
            int price2 = 1;

            //未購入の場合
            if (purchaseDatas == null)
            {
                //値段変更
                price1 = itemDatas.price * itemDatas.value;
                price2 = itemDatas.price * itemDatas.value;
            }
            //購入済みの場合
            else if (purchaseDatas != null)
            {
                //値段変更
                price1 = purchaseDatas.price * purchaseDatas.value;
                price2 = purchaseDatas.price * purchaseDatas.value;
            }

            //コインが足らない場合はスルー
            int price = price1 * price2;
            if (battleManager.playerData[2] <= price)
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                if (languageData == 0)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"コインが足らないネ";
                else if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Not enough coins";
            }
            //購入処理
            else
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[12]);//アイテム購入音
                if (languageData == 0)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"いつもありがとネ";
                else if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Thanks for everything";
                titleManager.TextBox.transform.Find("PurchaseButtonPanel").gameObject.SetActive(false);

                battleManager.shopPurchaseAdd();//未購入ならリストに追加
                battleManager.keyUpdate();//キー更新

                //売り切れ表示
                battleManager.ItemsPanel.transform.Find($"{battleManager.purchaseTmp_s2}").GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;//選択不可能
                battleManager.ItemsPanel.transform.Find($"{battleManager.purchaseTmp_s2}").GetChild(1).GetChild(2).gameObject.SetActive(true);//売り切れ表示
                //コイン消費
                battleManager.playerData[2] -= price;//変数格納
                battleManager.playerDataText[2].text = $"{battleManager.playerData[2]}";//テキスト変更
                ES3.Save<int>(battleManager.playerDataKey[2], battleManager.playerData[2]);//コインセーブ
            }
        }
        else if (battleManager.purchaseTmp_i2 == 1)
        {
            shopPurchaseButtonYes2();
        }
    }
    public void shopPurchaseButtonCancel()//キャンセル
    {
        titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
        if (languageData == 0)
            titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 なにか買うかい？ 」";
        if (languageData == 1)
            titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"welcome!";
        titleManager.TextBox.transform.Find("PurchaseButtonPanel").gameObject.SetActive(false);
    }
    public void shopChangeButton()
    {
        switch (transform.name)
        {
            case "B_ShopChange_R"://
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                titleManager.B_ShopChange_R.SetActive(false);
                titleManager.B_ShopChange_L.SetActive(true);
                battleManager.ItemsPanel.SetActive(false);
                battleManager.ItemsPanel2.SetActive(true);
                titleManager.B_ShopUpdate.SetActive(false);
                titleManager.Camera[3].transform.DOLocalMoveX(2, 0.3f).SetEase(Ease.Linear);
                if (languageData == 0)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 回復するかい？ 」";
                if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"welcome!";

                break;
            case "B_ShopChange_L"://
                titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
                titleManager.B_ShopChange_R.SetActive(true);
                titleManager.B_ShopChange_L.SetActive(false);
                battleManager.ItemsPanel.SetActive(true);
                battleManager.ItemsPanel2.SetActive(false);
                titleManager.B_ShopUpdate.SetActive(true);
                titleManager.Camera[3].transform.DOLocalMoveX(0, 0.3f).SetEase(Ease.Linear);
                if (languageData == 0)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 なにか買うかい？ 」";
                if (languageData == 1)
                    titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"welcome!";
                break;
            default:
                break;
        }

    }
    public void shopPurchaseButtonYes2()//購入2
    {
        //値段格納
        int i = battleManager.purchaseTmp_i;
        int price2 = (battleManager.shop2Price[i] * battleManager.shop2Value[i]) + (battleManager.shop2Price[i] * battleManager.shop2Value[i]);
        //コインが足らない場合はスルー
        if (battleManager.playerData[2] <= price2)
        {
            titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
            if (languageData == 0)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 コインが足らないゾ 」";
            else if (languageData == 1)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Not enough coins";
        }
        //購入処理
        else
        {
            //ハート
            if (i == 0)
            {
                //ハートが上限(9)の場合は終了
                if (battleManager.playerData[i + 4] >= 9)
                {
                    titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                    if (languageData == 0)
                        titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 ハート 」 が上限で買えないゾ";
                    else if (languageData == 1)
                        titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"You can't buy hearts at the upper limit";
                    return;
                }
                battleManager.playerData[i + 4] += battleManager.shop2Add[i];//変数格納
                //生成
                GameObject Obj;
                Obj = (GameObject)Instantiate(battleManager.Life_Pre, battleManager.Life_Pre.transform.position, Quaternion.identity);
                Obj.transform.SetParent(battleManager.LifePanel.transform.transform, false);

            }
            //ディフェンス
            else if (i == 1)
            {
                //ディフェンスが上限(10)の場合は終了
                if (battleManager.playerData[i + 4] >= 10)
                {
                    titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//no
                    if (languageData == 0)
                        titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"「 シールド 」 が上限で買えないゾ";
                    else if (languageData == 1)
                        titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"You can't buy shields at the upper limit";
                    return;
                }
                battleManager.playerData[i + 4] = battleManager.shop2Add[i];//変数格納
                battleManager.Defense_P.transform.Find("Slider").GetComponent<Slider>().value = battleManager.playerData[i + 4];
                battleManager.Defense_P.transform.Find("Slider/HS_Area/Handle/Text").GetComponent<TextMeshProUGUI>().text = $"{battleManager.playerData[i + 4]}";
            }

            //共通
            battleManager.shop2Value[i] += 1;
            ES3.Save<int>(battleManager.playerDataKey[i + 4], battleManager.playerData[i + 4]);//セーブ
            ES3.Save<int>(battleManager.playerShopKey[i], battleManager.shop2Value[i]);//セーブ

            titleManager.seSource.PlayOneShot(titleManager.seClip[12]);//アイテム購入音
            if (languageData == 0)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"いつもありがとう";
            else if (languageData == 1)
                titleManager.TextBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"Thanks for everything";
            titleManager.TextBox.transform.Find("PurchaseButtonPanel").gameObject.SetActive(false);

            //価格変更
            battleManager.ItemsPanel2.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{(battleManager.shop2Price[i] * battleManager.shop2Value[i]) + (battleManager.shop2Price[i] * battleManager.shop2Value[i])}";

            //コイン消費
            battleManager.playerData[2] -= price2;//変数格納
            battleManager.playerDataText[2].text = $"{battleManager.playerData[2]}";//テキスト変更
            ES3.Save<int>(battleManager.playerDataKey[2], battleManager.playerData[2]);//コインセーブ
        }
    }
    //====================================================================================================
    //キーボタン処理
    //====================================================================================================
    // 数字ボタン押下
    public void InputNumber(int number)
    {
        titleManager.seSource.PlayOneShot(titleManager.seClip[2]);//key決定音
        // 押下したボタンの数字を式欄に追記する
        battleManager.formula.text += $"{number}";

        //スコア加算用
        var purchaseDatas = battleManager.purchaseList.Find(x => x.name == $"{number}");
        battleManager.basePoint.Add(purchaseDatas.value);
    }
    // 割るボタン押下
    public void InputDivide()
    {
        // 数字が未入力か、すでに記号があればスルー
        for (int i = 0; i < symbol.Length; i++)
        {
            if (battleManager.formula.text == "" || $"{battleManager.formula.text.LastOrDefault()}" == symbol[i] || battleManager.formula.text.Contains(symbol[battleManager.formulaSymbol]))
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
                return;
            }
        }
        // /を式欄に追記する
        titleManager.seSource.PlayOneShot(titleManager.seClip[3]);//記号
        battleManager.formulaSymbol = 3;
        battleManager.formula.text += $"/";
        battleManager.formulaSymbol_S = $"÷";
        battleManager.basePoint.Add(0);
    }
    // 掛けるボタン押下
    public void InputMultiplied()
    {
        // 数字が未入力か、すでに記号があればスルー
        for (int i = 0; i < symbol.Length; i++)
        {
            if (battleManager.formula.text == "" || $"{battleManager.formula.text.LastOrDefault()}" == symbol[i] || battleManager.formula.text.Contains(symbol[battleManager.formulaSymbol]))
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
                return;
            }
        }

        // *を式欄に追記する
        titleManager.seSource.PlayOneShot(titleManager.seClip[3]);//記号
        battleManager.formulaSymbol = 2;
        battleManager.formula.text += $"*";
        battleManager.formulaSymbol_S = $"×";
        battleManager.basePoint.Add(0);
    }
    // 足すボタン押下
    public void InputPlus()
    {
        // 数字が未入力か、すでに記号があればスルー
        for (int i = 0; i < symbol.Length; i++)
        {
            if (battleManager.formula.text == "" || $"{battleManager.formula.text.LastOrDefault()}" == symbol[i] || battleManager.formula.text.Contains(symbol[battleManager.formulaSymbol]))
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
                return;
            }
        }

        // +を式欄に追記する
        titleManager.seSource.PlayOneShot(titleManager.seClip[3]);//記号
        battleManager.formulaSymbol = 0;
        battleManager.formula.text += $"+";
        battleManager.formulaSymbol_S = $"+";
        battleManager.basePoint.Add(0);
    }
    // 引くボタン押下
    public void InputMinus()
    {
        // 数字が未入力か、すでに記号があればスルー
        for (int i = 0; i < symbol.Length; i++)
        {
            if (battleManager.formula.text == "" || $"{battleManager.formula.text.LastOrDefault()}" == symbol[i] || battleManager.formula.text.Contains(symbol[battleManager.formulaSymbol]))
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
                return;
            }
        }
        // -を式欄に追記する
        titleManager.seSource.PlayOneShot(titleManager.seClip[3]);//記号
        battleManager.formulaSymbol = 1;
        battleManager.formula.text += $"-";
        battleManager.formulaSymbol_S = $"-";
        battleManager.basePoint.Add(0);
    }
    // クリアボタン押下
    public void InputClear()
    {
        titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//keyクリア音
        //初期化
        battleManager.formula.text = "";
        battleManager.basePoint = new List<int>();
    }
    // 一文字削除ボタン押下
    public void InputDell()
    {
         // 数字が未入力であればスルー
        if (battleManager.formula.text == "" )
        {
            titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
            return;
        }

        titleManager.seSource.PlayOneShot(titleManager.seClip[1]);//keyクリア音
        //末尾一文字削除
        battleManager.formula.text = battleManager.formula.text.Substring(0, battleManager.formula.text.Length - 1);
        battleManager.basePoint.RemoveAt(battleManager.basePoint.Count - 1);
    }
    // 計算ボタン押下
    public void InputEqual()
    {
        // 数字が未入力か、すでに記号があればスルー
        for (int i = 0; i < symbol.Length; i++)
        {
            if (battleManager.formula.text == "" || $"{battleManager.formula.text.LastOrDefault()}" == symbol[i] || !battleManager.formula.text.Contains(symbol[battleManager.formulaSymbol]))
            {
                titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
                return;
            }
        }

        // 入力した式を分ける
        string[] inputString = battleManager.formula.text.Split(symbol[battleManager.formulaSymbol]);
        int leftNumber = int.Parse(inputString[0]);
        int rightNumber = int.Parse(inputString[1]);

        //スコア判定用
        battleManager.scoreArray[0] = leftNumber;
        battleManager.scoreArray[1] = rightNumber;
        battleManager.scoreArray[2] = battleManager.formulaSymbol;

        // 割られる数がゼロならスルー
        if (rightNumber == 0)
        {
            titleManager.seSource.PlayOneShot(titleManager.seClip[4]);//スルー音
            return;
        }

        titleManager.seSource.PlayOneShot(titleManager.seClip[5]);//エンター音
        int result = 0;
        int remainder = 0;

        if (battleManager.formulaSymbol == 0)//足す
        {
            result = leftNumber + rightNumber;
        }
        else if (battleManager.formulaSymbol == 1)//引く
        {
            result = leftNumber - rightNumber;
        }
        else if (battleManager.formulaSymbol == 2)//掛ける
        {
            result = leftNumber * rightNumber;
        }
        else if (battleManager.formulaSymbol == 3)//割る
        {
            result = leftNumber / rightNumber;
            // 余り
            remainder = leftNumber % rightNumber;
        }

        battleManager.answerList.Add(result);
        battleManager.scoreList.Add(battleManager.scoreArray);

        //クリア時の表示用
        //ボーナス
        var purchaseDatas = battleManager.purchaseList.Find(x => x.name == $"{battleManager.formulaSymbol_S}");
        
        int point = 0;
        for (int i = 0; i < battleManager.basePoint.Count; i++)
        {
            point += battleManager.basePoint[i];
        }
        battleManager.basePointCount.Add(point);//永久(数)
        battleManager.aanswerCount.Add($"{battleManager.formula.text}");//永久(式)
        battleManager.formulaSymbolCount.Add($"{battleManager.formulaSymbol_S}");//永久(記号)
        battleManager.formulaSymbolPointCount.Add(purchaseDatas.value);//永久(記号のvalue)
        battleManager.basePoint_L.Add(point);
        battleManager.formulaSymbolPoint_L.Add(purchaseDatas.value);
        // 計算後クリア
        battleManager.formula.text = $"";
        battleManager.basePoint = new List<int>();
        //生成
        GameObject Obj;
        Obj = (GameObject)Instantiate(battleManager.AnswerPanelPre, battleManager.AnswerPanelPre.transform.position, Quaternion.identity);
        Obj.transform.SetParent(battleManager.fieldContent.transform, false);
        Obj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"{result}";
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
        Emphasis1 = (GameObject)Instantiate(titleManager.Emphasispre[0], titleManager.Emphasispre[0].transform.position, titleManager.Emphasispre[0].transform.rotation);//生成
        Emphasis2 = (GameObject)Instantiate(titleManager.Emphasispre[1], titleManager.Emphasispre[1].transform.position, titleManager.Emphasispre[1].transform.rotation);//生成
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
}
