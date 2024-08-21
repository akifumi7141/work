using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    //スクリプト
    private GameObject Script;
    TitleManager titleManager;

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        transform.localScale = new Vector3(-1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(titleManager.Camera[2].transform);
    }
}
