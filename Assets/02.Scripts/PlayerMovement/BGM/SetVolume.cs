using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider BGMslider;
    public Slider SFXslider;

    private void Start()
    {
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        mixer.SetFloat("EffectVolume", PlayerPrefs.GetFloat("EffectVolume"));

        if (BGMslider != null && SFXslider != null)
        {
            BGMslider.value = PlayerPrefs.GetFloat("MusicSlider", 1.0f);
            SFXslider.value = PlayerPrefs.GetFloat("EffectSlider", 1.0f);
        }
    }

    public void SetBGMLevel(float sliderValue = 1)
    {
        float outFloat;

        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        //PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        mixer.GetFloat("MusicVolume", out outFloat);
        PlayerPrefs.SetFloat("MusicVolume", outFloat);
        PlayerPrefs.SetFloat("MusicSlider", sliderValue);
    }

    public void SetSFXLevel(float sliderValue = 1)
    {
        float outFloat;

        mixer.SetFloat("EffectVolume", Mathf.Log10(sliderValue) * 20);
        //PlayerPrefs.SetFloat("EffectVolume", sliderValue);
        mixer.GetFloat("EffectVolume", out outFloat);
        PlayerPrefs.SetFloat("EffectVolume", outFloat);
        PlayerPrefs.SetFloat("EffectSlider", sliderValue);
    }
}
