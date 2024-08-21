using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class languageText : MonoBehaviour
{
    public string[] language_JA;//日本語
    public string[] language_EN;//英語
    public TextMeshProUGUI[] language_Text;//日本語
    public TMP_FontAsset[] font;

    void Start()
    {
        int languageData = ES3.Load<int>("LANGUAGE", defaultValue: 0);
        for (int i = 0; i < language_Text.Length; i++)
        {
            language_Text[i].font = font[languageData];
            if(languageData == 0)
            language_Text[i].text = language_JA[i];
            else if (languageData == 1)
                language_Text[i].text = language_EN[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
