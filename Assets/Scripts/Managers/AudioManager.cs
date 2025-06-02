using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region Instance
    public static AudioManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Audio Params
    [Header("Audio")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _masterParamName = "Master";
    [Range(0.0f, 1.0f)][SerializeField] private float _masterInitValue = 0.7f;
    public float MasterInitValue
    {
        get { return _masterInitValue; }
        private set { _masterInitValue = value; }
    }
    [SerializeField] private string _musicParamName = "Music";
    [Range(0.0f, 1.0f)][SerializeField] private float _musicInitValue = 0.5f;
    public float MusicInitValue
    {
        get { return _musicInitValue; }
        private set { _musicInitValue = value; }
    }
    [SerializeField] private string _sfxParamName = "SFX";
    [Range(0.0f, 1.0f)][SerializeField] private float _sfxInitValue = 1.0f;
    public float SfxInitValue
    {
        get { return _sfxInitValue; }
        private set { _sfxInitValue = value; }
    }
    [SerializeField] private string _uiParamName = "UI";
    [Range(0.0f, 1.0f)][SerializeField] private float _uiInitValue = 0.8f;
    public float UiInitValue
    {
        get { return _uiInitValue; }
        private set { _uiInitValue = value; }
    }
    #endregion

    private AudioSource _musicSource;

    private void Start()
    {
        _musicSource = GetComponent<AudioSource>();

        ModifyMasterVolume(_masterInitValue);
        ModifyMusicVolume(_musicInitValue);
        ModifySFXVolume(_sfxInitValue);
        ModifyUIVolume(_uiInitValue);
    }

    public void ModifyMasterVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_masterParamName, Mathf.Log10(value) * 20.0f);
    }

    public void ModifyMusicVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_musicParamName, Mathf.Log10(value) * 20.0f);
    }

    public void ModifySFXVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_sfxParamName, Mathf.Log10(value) * 20.0f);
    }

    public void ModifyUIVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_uiParamName, Mathf.Log10(value) * 20.0f);
    }

    public void PlayMusicClip(AudioClip clip)
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }

        _musicSource.clip = clip;

        _musicSource.Play();
    }
}
