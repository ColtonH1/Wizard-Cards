using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public bool isFullScreen;
    public Toggle fullScreenToggle;

    private void Awake()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
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
