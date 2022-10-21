using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider VolMasterSlider;
    [SerializeField] Slider VolMusicSlider;
    [SerializeField] Slider VolSoundSlider;
    float valueMaster;
    float valueMusic;
    float valueSound;
    void Start()
    {
        
        mixer.GetFloat("VolMaster", out valueMaster);
        VolMasterSlider.value = DecibelToLinear(valueMaster);

        mixer.GetFloat("VolMusic", out valueMusic);
        VolMusicSlider.value = DecibelToLinear(valueMusic);

        mixer.GetFloat("VolSound", out valueSound);
        VolSoundSlider.value = DecibelToLinear(valueSound);


    }
    public void SetVolMaster(float sliderValue)
    {
        mixer.SetFloat("VolMaster", LinearToDecibel(sliderValue)); //el vol master es como se llama el parametro expuesto
    }
    public void SetVolSound(float sliderValue)
    {
        mixer.SetFloat("VolSound", LinearToDecibel(sliderValue)); //el vol master es como se llama el parametro expuesto
    }
    public void SetVolMusic(float sliderValue)
    {
        mixer.SetFloat("VolMusic", LinearToDecibel(sliderValue)); //el vol master es como se llama el parametro expuesto
    }
    private float LinearToDecibel(float linear)
    {
        float dB;
        
        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20.0f);

        return linear;
    }
}
