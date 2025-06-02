using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderState : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _uiSlider;

    private void Start()
    {
        _masterSlider.value = AudioManager.Instance.MasterInitValue;
        _musicSlider.value = AudioManager.Instance.MusicInitValue;
        _sfxSlider.value = AudioManager.Instance.SfxInitValue;
        _uiSlider.value = AudioManager.Instance.UiInitValue;
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.ModifyMasterVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.ModifyMusicVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.ModifySFXVolume(value);
    }

    public void SetUIVolume(float value)
    {
        AudioManager.Instance.ModifyUIVolume(value);
    }
}
