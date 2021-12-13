using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownMenu : MonoBehaviour
{
    public GameObject audioManagerGO;
    private AudioManager audioManager;

    [SerializeField] GameObject menuOptionsGO;
    private bool isOpen;

    /*private bool isPaused;

    public TMP_Dropdown menuOptions;
    public Sprite[] menuSprites;
    private List<TMP_Dropdown.OptionData> menuItems = new List<TMP_Dropdown.OptionData>();

    public Sprite muteImage;
    public Sprite soundImage;
    public Sprite pauseImage;
    public Sprite playImage;

    private string muteImageName;
    private string soundImageName;
    private string pauseImageName;
    private string playImageName;*/

    private void Start()
    {
        //isPaused = false;

        audioManagerGO = GameObject.Find("AudioManager Variant");
        audioManager = audioManagerGO.GetComponent<AudioManager>();

        /*
        menuOptions.ClearOptions();

        muteImageName = muteImage.name;
        soundImageName = soundImage.name;
        pauseImageName = pauseImage.name;
        playImageName = playImage.name;


        foreach(var sprite in menuSprites)
        {
            var spriteOption = new TMP_Dropdown.OptionData(sprite);
            menuItems.Add(spriteOption);
        }

        menuOptions.AddOptions(menuItems);
        menuOptions.options.Add(new TMP_Dropdown.OptionData(pauseImage));
        menuOptions.options.Add(new TMP_Dropdown.OptionData(soundImage));
        */
    }

    public void Clicked()
    {
        if(isOpen)
        {
            menuOptionsGO.SetActive(false);
            isOpen = false;
        }
        else
        {
            menuOptionsGO.SetActive(true);
            isOpen = true;
        }
    }



    public void PauseTime()
    {
        /*
        isPaused = true;
        
        int index = -1;
        for(int i = 0; i < menuOptions.options.Count; i++)
        {
            index = menuOptions.options.FindIndex((i) => { return i.text.Equals(pauseImageName); });
        }

        if (index == -1)
            Debug.LogError("Drop down menu contains no options or searched for option cannot be found");

        menuOptions.options.RemoveAt(index);
        menuOptions.options.Insert(index, new TMP_Dropdown.OptionData(playImage));
        */

        Time.timeScale = 0.0f;

    }

    public void ResumeTime()
    {
        /*
        int index = -1;
        for (int i = 0; i < menuOptions.options.Count; i++)
        {
            index = menuOptions.options.FindIndex((i) => { return i.text.Equals(playImageName); });
        }

        if (index == -1)
            Debug.LogError("Drop down menu contains no options or searched for option cannot be found");

        menuOptions.options.RemoveAt(index);
        menuOptions.options.Insert(index, new TMP_Dropdown.OptionData(pauseImage));
        */

        Time.timeScale = 1.0f;
    }

    public void Mute()
    {
        /*
        int index = -1;
        for (int i = 0; i < menuOptions.options.Count; i++)
        {
            index = menuOptions.options.FindIndex((i) => { return i.text.Equals(soundImageName); });
        }

        if (index == -1)
            Debug.LogError("Drop down menu contains no options or searched for option cannot be found");

        menuOptions.options.RemoveAt(index);
        menuOptions.options.Insert(index, new TMP_Dropdown.OptionData(muteImage));
        */

        audioManager.MuteAudio();
    }

    public void Unmute()
    {
        /*
        int index = -1;
        for (int i = 0; i < menuOptions.options.Count; i++)
        {
            index = menuOptions.options.FindIndex((i) => { return i.text.Equals(muteImageName); });
        }

        if (index == -1)
            Debug.LogError("Drop down menu contains no options or searched for option cannot be found");

        menuOptions.options.RemoveAt(index);
        menuOptions.options.Insert(index, new TMP_Dropdown.OptionData(soundImage));
        */

        audioManager.UnmuteAudio();
    }
}
