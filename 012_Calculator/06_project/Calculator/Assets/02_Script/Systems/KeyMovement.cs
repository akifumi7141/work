using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameObject Script;
    TitleManager titleManager;
    BattleManager battleManager;

    public Transform keyParent;
    public Vector2 prevPos; //保存しておく初期position
    public RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    public RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        battleManager = Script.GetComponent<BattleManager>();

        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;

    }
    // ドラッグを始めるときに行う処理
    public void OnBeginDrag(PointerEventData eventData) 
    {
        prevPos = rectTransform.anchoredPosition;
        keyParent = transform.parent;
        transform.SetParent(keyParent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false; // blocksRaycastsをオフにする
    }
    // ドラッグしてる時に起こす処理
    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition = eventData.position;
        transform.SetParent(titleManager.PearentPanel.transform, false);//一時的に別の親に入る
        //prevPos = rectTransform.anchoredPosition;
    }
    // 離したときに行う処理
    public void OnEndDrag(PointerEventData eventData) 
    {
        transform.SetParent(keyParent, false);//元の親に戻る
        rectTransform.anchoredPosition = Vector3.zero;
        GetComponent<CanvasGroup>().blocksRaycasts = true; // blocksRaycastsをオンにする
    }
}