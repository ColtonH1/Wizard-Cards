using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownMenu : MonoBehaviour
{
    public GameObject audioManagerGO;
    private AudioManager audioManager;

    public Image arrow;
    public Sprite[] arrowArr;

    [SerializeField] GameObject menuOptionsGO;
    private bool isOpen;

    private void Start()
    {
        //isPaused = false;

        audioManagerGO = GameObject.Find("AudioManager Variant");
        audioManager = audioManagerGO.GetComponent<AudioManager>();

        arrow.sprite = arrowArr[1];
    }

    public void Clicked()
    {
        if(isOpen)
        {
            menuOptionsGO.SetActive(false);
            isOpen = false;
            arrow.sprite = arrowArr[1];
        }
        else
        {
            menuOptionsGO.SetActive(true);
            isOpen = true;
            arrow.sprite = arrowArr[0];
        }
    }



    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
    }

    public void Mute()
    {
        audioManager.MuteAudio();
    }

    public void Unmute()
    {

        audioManager.UnmuteAudio();
    }
}
