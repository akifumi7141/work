using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class language : MonoBehaviour
{
    public GameObject language_P;
    public string languageDataKey = "LANGUAGE";
    public int languageData = 0;

    void Start()
    {
        languageData = ES3.Load<int>(languageDataKey, defaultValue: 0);
        var onValueChanged = language_P.transform.GetChild(languageData).GetComponent<Toggle>().onValueChanged;
        language_P.transform.GetChild(languageData).GetComponent<Toggle>().onValueChanged = new Toggle.ToggleEvent();
        language_P.transform.GetChild(languageData).GetComponent<Toggle>().isOn = true;
        language_P.transform.GetChild(languageData).GetComponent<Toggle>().onValueChanged = onValueChanged;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
