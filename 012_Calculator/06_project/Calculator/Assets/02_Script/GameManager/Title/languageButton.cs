using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class languageButton : MonoBehaviour
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
    public void languageChange(int i)
    {
        if (this.GetComponent<Toggle>().isOn == true) {
            language.languageData = i;

        }
    }
}
