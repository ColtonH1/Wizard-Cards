using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    public SettingsSO settings;
    private string playerName;
    public TMP_InputField playerNameInput;
    private Toggle[] difficulty_Toggle;
    private Toggle[] character_Toggle;

    public static PlayerSettings instance;


    private void Awake()
    {
        //make this a singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        //check that we are in the right scene
        if(SceneManager.GetActiveScene().name == "PlayerSettings")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
            
    }

    void Start()
    {
        
    }

    public void OnStart()
    {
        playerName = playerNameInput.text;
        settings.playerName = playerName;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        DifficultyToggleGroup(scene); //display toggle group on player settings page
        CharacterToggleGroup(scene);

    }

    private void DifficultyToggleGroup(Scene scene)
    {
        if (scene.name == "PlayerSettings")
        {
            //create toggle group and find the corrosponding toggles in the scene
            difficulty_Toggle = new Toggle[3];
            difficulty_Toggle[0] = GameObject.Find("Easy_tgle").GetComponent<Toggle>();
            difficulty_Toggle[1] = GameObject.Find("Medium_tgle").GetComponent<Toggle>();
            difficulty_Toggle[2] = GameObject.Find("Hard_tgle").GetComponent<Toggle>();

            //cycle through the toggles, and make sure the one that should be on, is on.
            for (int i = 0; i < 3; i++)
            {
                if (i == settings.difficulty)
                {
                    difficulty_Toggle[i].GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    difficulty_Toggle[i].GetComponent<Toggle>().isOn = false;
                }
            }

            //when toggle value is changed, update the difficulty
            difficulty_Toggle[0].onValueChanged.AddListener((isOn) => SetEasyDifficulty(isOn));
            difficulty_Toggle[1].onValueChanged.AddListener((isOn) => SetMediumDifficulty(isOn));
            difficulty_Toggle[2].onValueChanged.AddListener((isOn) => SetHardDifficulty(isOn));
        }
    }


    #region Difficulty Change
    //set the difficulty when called
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            settings.difficulty = 0;
            settings.enemyCharNum = 0;
        }

    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            settings.difficulty = 1;
            settings.enemyCharNum = 1;
        }
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            settings.difficulty = 2;
            settings.enemyCharNum = 2;
        }
    }
    #endregion

    private void CharacterToggleGroup(Scene scene)
    {
        if (scene.name == "PlayerSettings")
        {
            GridLayoutGroup characterTgle = GameObject.Find("Character_group_tgle").GetComponent<GridLayoutGroup>();
            int numOfChars = characterTgle.transform.childCount;
            //create toggle group and find the corrosponding toggles in the scene
            character_Toggle = new Toggle[numOfChars];
            for(int i = 0; i < numOfChars; i++)
            {
                character_Toggle[i] = characterTgle.transform.GetChild(i).GetComponent<Toggle>();
            }
            /*
            character_Toggle[0] = GameObject.Find("Character 1").GetComponent<Toggle>();
            character_Toggle[1] = GameObject.Find("Character 2").GetComponent<Toggle>();
            character_Toggle[2] = GameObject.Find("Character 3").GetComponent<Toggle>();
            character_Toggle[3] = GameObject.Find("Character 4").GetComponent<Toggle>();
            character_Toggle[4] = GameObject.Find("Character 5").GetComponent<Toggle>();
            character_Toggle[5] = GameObject.Find("Character 6").GetComponent<Toggle>();
            */

            //cycle through the toggles, and make sure the one that should be on, is on.
            for (int i = 0; i < numOfChars; i++)
            {
                if(i == settings.playerCharNum)
                {
                    character_Toggle[i].GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    character_Toggle[i].GetComponent<Toggle>().isOn = false;
                }
            }

            //when toggle value is changed, update the character sprite
            character_Toggle[0].onValueChanged.AddListener((isOn) => SetChar1(isOn));
            character_Toggle[1].onValueChanged.AddListener((isOn) => SetChar2(isOn));
            character_Toggle[2].onValueChanged.AddListener((isOn) => SetChar3(isOn));
            character_Toggle[3].onValueChanged.AddListener((isOn) => SetChar4(isOn));
            character_Toggle[4].onValueChanged.AddListener((isOn) => SetChar5(isOn));
        }
    }

    #region Character Change
    //set the player character when called
    public void SetChar1(bool isOn)
    {
        if (isOn)
            settings.playerCharNum = 0;
            //settings.playerChar = settings.playerSprites[0];
    }

    public void SetChar2(bool isOn)
    {
        if (isOn)
            settings.playerCharNum = 1;
        //settings.playerChar = settings.playerSprites[1];
    }

    public void SetChar3(bool isOn)
    {
        if (isOn)
            settings.playerCharNum = 2;
        //settings.playerChar = settings.playerSprites[2];
    }

    public void SetChar4(bool isOn)
    {
        if (isOn)
            settings.playerCharNum = 3;
        //settings.playerChar = settings.playerSprites[3];
    }

    public void SetChar5(bool isOn)
    {
        if (isOn)
            settings.playerCharNum = 4;
        //settings.playerChar = settings.playerSprites[4];
    }
    #endregion

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
