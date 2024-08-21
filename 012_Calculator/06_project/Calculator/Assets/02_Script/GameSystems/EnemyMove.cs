using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EnemyMove : MonoBehaviour
{
    private GameObject Script;
    TitleManager titleManager;
    BattleManager battleManager;

    public Tweener tweener;
    public bool isMove = true;

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        battleManager = Script.GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (battleManager.playerData[3] == 0 && isMove)
        {
            tweener.Pause();
            isMove = false;
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        int flag = 0;
        //ライフが0の場合スキップ
        if (battleManager.playerData[4] == 0)
        {
            return;
        }
        //プレイヤーでなければスキップ
        if (collision.tag != "Player")
        {
            return;
        }
        //攻撃判定
        if (battleManager.answerList.Count > 0)
        {
            //正解
            if (battleManager.answerList[0] == battleManager.enemyList[0])
            {
                //敵のオブジェクト削除
                battleManager.characterStagePre.GetComponent<Animator>().SetTrigger("Attack01_T");//モーション変更
                titleManager.seSource.PlayOneShot(titleManager.seClip[6]);//攻撃音
                Destroy(this.gameObject);
                battleManager.comboTrue();
                flag = 0;
            }
            //不正解
            else
            {
                this.GetComponent<Animator>().SetTrigger("Attack01_T");//敵モーション変更
                battleManager.characterStagePre.GetComponent<Animator>().SetTrigger("Die01_T");//モーション変更
               
                titleManager.seSource.PlayOneShot(titleManager.seClip[10]);//ダメージ音
                //ライフマイナス
                battleManager.playerData[4] -= 1;
                Destroy(battleManager.LifePanel.transform.GetChild(battleManager.playerData[4]).gameObject);
                ES3.Save<int>(battleManager.playerDataKey[4], battleManager.playerData[4]);

                battleManager.comboFalse();//コンボ途切れ
                flag = 1;
            }

        }
        //答えを一つも入力していない場合スルー
        else if (battleManager.answerList.Count <= 0)
        {
            //モーション変更
            battleManager.characterStagePre.GetComponent<Animator>().SetTrigger("DefenseHit_T");
            titleManager.seSource.PlayOneShot(titleManager.seClip[9]);//スルー音
            //耐久
            battleManager.playerData[5] -= 1;
            battleManager.Defense_P.transform.Find("Slider").GetComponent<Slider>().value = battleManager.playerData[5];
            battleManager.Defense_P.transform.Find("Slider/HS_Area/Handle/Text").GetComponent<TextMeshProUGUI>().text = $"{battleManager.playerData[5]}";
            ES3.Save<int>(battleManager.playerDataKey[5], battleManager.playerData[5]);//セーブ

            battleManager.comboFalse();//コンボ途切れ
            flag = 2;
        }
        battleManager.scoreCount(flag);
        //敵リストの先頭を削除
        battleManager.enemyList.RemoveAt(0);

        if (flag != 2)
        {
            //攻撃リストの先頭と表示削除
            battleManager.answerList.RemoveAt(0);
            //基礎ポイントの先頭を削除
            battleManager.basePoint_L.RemoveAt(0);
            //記号ポイントの先頭を削除
            battleManager.formulaSymbolPoint_L.RemoveAt(0);
            //答えオブジェクト削除
            Destroy(battleManager.fieldContent.transform.GetChild(0).gameObject);
        }

        //事後処理
        battleManager.GameClearCount();//クリアカウント
        battleManager.GameOverCount();//ゲームオーバー
    }

}
