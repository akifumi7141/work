using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// フィールドにアタッチするクラス
public class DropPlace : MonoBehaviour, IDropHandler
{
    private GameObject Script;
    TitleManager titleManager;

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
    }

    public void OnDrop(PointerEventData eventData) // ドロップされた時に行う処理
    {
        KeyMovement key = eventData.pointerDrag.GetComponent<KeyMovement>(); // ドラッグしてきた情報からCardMovementを取得
        if (key != null) // もしキーがあれば、
        {
            Transform children = this.transform.GetComponentInChildren<Transform>();
            //子要素がいなければ終了
            if (children.childCount != 0)
            {
                if (this.name != "Content")
                {
                    this.transform.GetChild(0).transform.SetParent(key.keyParent, false);
                }
            }
            key.keyParent = this.transform; // キーの親要素を自分（アタッチされてるオブジェクト）にする
            titleManager.seSource.PlayOneShot(titleManager.seClip[0]);//yes
        }
    }
}