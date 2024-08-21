using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    public UnityEngine.Audio.AudioMixer mixer;
    private GameObject Script;
    TitleManager titleManager;

    void Start()
    {
        Script = GameObject.Find("GameManager");
        titleManager = Script.GetComponent<TitleManager>();
        titleManager.MusicSlider.value = PlayerPrefs.GetFloat(TitleManager.Music, 0);
        titleManager.SESlider.value = PlayerPrefs.GetFloat(TitleManager.SE, 0);
    }

    public void MusicVol(Slider slider)
    {
        mixer.SetFloat("MusicVolume", slider.value);
        TitleManager.fMusic = slider.value;
    }
    public void SEVol(Slider slider)
    {
        mixer.SetFloat("SEVolume", slider.value);
        TitleManager.fSE = slider.value;
    }

    internal void GetFloat(string floatName, out float currVal)
    {
        throw new NotImplementedException();
    }

    internal void SetFloat(string floatName, double x)
    {
        throw new NotImplementedException();
    }
}
