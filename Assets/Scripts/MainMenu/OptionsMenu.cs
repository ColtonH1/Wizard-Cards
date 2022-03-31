using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public SettingsSO settings;

    public AudioMixer audioMixer;
    public bool isFullScreen;
    public Toggle fullScreenToggle;
    public Slider audioSlider;

    private void Awake()
    {
        settings.screenToggle = Screen.fullScreen;
    }

    private void Start()
    {
        audioSlider.value = settings.soundSetting;
        fullScreenToggle.SetIsOnWithoutNotify(settings.screenToggle);
        isFullScreen = settings.screenToggle;
        audioSlider.onValueChanged.AddListener(UpdateSlider);
    }

    private void UpdateSlider(float soundChange)
    {
        settings.soundSetting = audioSlider.value;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10 (volume) * 20);
    }

    public void SetFullScreen()
    {
        isFullScreen = !isFullScreen;
        if (isFullScreen)
            SetExclusiveFullScreen();
        else
            SetFullScreenWindow();
        Debug.Log("Full screen = " + isFullScreen);
    }

    public void SetExclusiveFullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void SetFullScreenWindow()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}
