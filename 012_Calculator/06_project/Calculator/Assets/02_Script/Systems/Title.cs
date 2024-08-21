using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    //スクリプト
    private GameObject Script;
    language language;

    void Start()
    {
        //スクリプト格納
        Script = GameObject.Find("GameManager");
        language = Script.GetComponent<language>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tapToMain()
    {
        //セーブ
        ES3.Save<int>(language.languageDataKey, language.languageData);//言語セーブ
        SceneNavigator.Instance.Change("03_Main", 0.5f);
    }
}
