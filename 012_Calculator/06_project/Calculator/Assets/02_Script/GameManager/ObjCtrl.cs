using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCtrl : MonoBehaviour
{
    private GameObject Script;
    BattleManager battleManager;

    //回転用
    Vector2 sPos;   //タッチした座標
    Quaternion sRot;//タッチしたときの回転
    float wid, hei, diag;  //スクリーンサイズ
    float tx, ty;    //変数

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        battleManager = Script.GetComponent<BattleManager>();

        wid = Screen.width;
        hei = Screen.height;
        diag = Mathf.Sqrt(Mathf.Pow(wid, 2) + Mathf.Pow(hei, 2));
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            //回転
            Touch t1 = Input.GetTouch(0);
            if (t1.phase == TouchPhase.Began)
            {
                sPos = t1.position;
                sRot = battleManager.characterHomePre.transform.rotation;
            }
            else if (t1.phase == TouchPhase.Moved || t1.phase == TouchPhase.Stationary)
            {
                tx = (t1.position.x - sPos.x) / wid; //横移動量(-1<tx<1)
                //ty = (t1.position.y - sPos.y) / hei; //縦移動量(-1<ty<1)
                battleManager.characterHomePre.transform.rotation = sRot;
                battleManager.characterHomePre.transform.Rotate(new Vector3(90 * ty, -90 * tx, 0), Space.World);
            }
        }
    }
}