using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private GameObject Script;
    TitleManager titleManager;
    BattleManager battleManager;


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
        // 左クリック
        if (Input.GetKeyDown(KeyCode.A))
        {
            battleManager.characterStagePre.GetComponent<Animator>().SetTrigger("Attack01_T");
            Debug.Log("左クリック");
        }
    }
}
