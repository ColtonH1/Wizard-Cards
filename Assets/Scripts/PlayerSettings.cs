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
    public enum Difficulties { EASY, MEDIUM, HARD };
    public enum EnemyCharacter { ENEMYCHAR1, ENEMYCHAR2, ENEMYCHAR3 };
    public enum PlayerCharacter { PLAYERCHAR1, PLAYERCHAR2, PLAYERCHAR3, PLAYERCHAR4, PLAYERCHAR5 };
    private string playerName;
    public TMP_InputField playerNameInput;
    public static Difficulties difficulty = Difficulties.EASY;
    public static EnemyCharacter enemyCharacter = EnemyCharacter.ENEMYCHAR1;
    public static PlayerCharacter playerCharacter = PlayerCharacter.PLAYERCHAR1;
    private Toggle[] m_Toggle;

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
            m_Toggle = new Toggle[3];
            m_Toggle[0] = GameObject.Find("Easy_tgle").GetComponent<Toggle>();
            m_Toggle[1] = GameObject.Find("Medium_tgle").GetComponent<Toggle>();
            m_Toggle[2] = GameObject.Find("Hard_tgle").GetComponent<Toggle>();

            //cycle through the toggles, and make sure the one that should be on, is on.
            for (int i = 0; i < 3; i++)
            {
                if (i == settings.difficulty)
                {
                    m_Toggle[i].GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    m_Toggle[i].GetComponent<Toggle>().isOn = false;
                }
            }

            /*
            //make sure only the correct toggle is turned on and the other two are turned off when scene is loaded
            switch (difficulty)
            {
                case Difficulties.EASY:
                    m_Toggle[1].GetComponent<Toggle>().isOn = false;
                    m_Toggle[2].GetComponent<Toggle>().isOn = false;
                    m_Toggle[0].GetComponent<Toggle>().isOn = true;
                    break;
                case Difficulties.MEDIUM:
                    m_Toggle[0].GetComponent<Toggle>().isOn = false;
                    m_Toggle[2].GetComponent<Toggle>().isOn = false;
                    m_Toggle[1].GetComponent<Toggle>().isOn = true;
                    break;
                case Difficulties.HARD:
                    m_Toggle[0].GetComponent<Toggle>().isOn = false;
                    m_Toggle[1].GetComponent<Toggle>().isOn = false;
                    m_Toggle[2].GetComponent<Toggle>().isOn = true;
                    break;
                default:
                    Debug.LogError("No difficulty set. Game not valid!");
                    break;
            }
            */

            //when toggle value is changed, update the difficulty
            m_Toggle[0].onValueChanged.AddListener((isOn) => SetEasyDifficulty(isOn));
            m_Toggle[1].onValueChanged.AddListener((isOn) => SetMediumDifficulty(isOn));
            m_Toggle[2].onValueChanged.AddListener((isOn) => SetHardDifficulty(isOn));
        }
    }


    #region Difficulty Change
    //set the difficulty when called
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
        {
            settings.difficulty = 0;
            settings.enemyChar = settings.enemySprites[0];
            //difficulty = Difficulties.EASY;
            //enemyCharacter = EnemyCharacter.ENEMYCHAR1;
        }

    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
        {
            settings.difficulty = 1;
            settings.enemyChar = settings.enemySprites[1];
            //difficulty = Difficulties.MEDIUM;
            //enemyCharacter = EnemyCharacter.ENEMYCHAR2;
        }
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
        {
            settings.difficulty = 2;
            settings.enemyChar = settings.enemySprites[2];
            //difficulty = Difficulties.HARD;
            //enemyCharacter = EnemyCharacter.ENEMYCHAR3;
        }
    }
    #endregion

    private void CharacterToggleGroup(Scene scene)
    {
        if (scene.name == "PlayerSettings")
        {
            //create toggle group and find the corrosponding toggles in the scene
            m_Toggle = new Toggle[5];
            m_Toggle[0] = GameObject.Find("Character 1").GetComponent<Toggle>();
            m_Toggle[1] = GameObject.Find("Character 2").GetComponent<Toggle>();
            m_Toggle[2] = GameObject.Find("Character 3").GetComponent<Toggle>();
            m_Toggle[3] = GameObject.Find("Character 4").GetComponent<Toggle>();
            m_Toggle[4] = GameObject.Find("Character 5").GetComponent<Toggle>();

            //cycle through the toggles, and make sure the one that should be on, is on.
            for(int i = 0; i < 5; i++)
            {
                if(i == settings.playerCharNum)
                {
                    m_Toggle[i].GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    m_Toggle[i].GetComponent<Toggle>().isOn = false;
                }
            }

            //when toggle value is changed, update the character sprite
            m_Toggle[0].onValueChanged.AddListener((isOn) => SetChar1(isOn));
            m_Toggle[1].onValueChanged.AddListener((isOn) => SetChar2(isOn));
            m_Toggle[2].onValueChanged.AddListener((isOn) => SetChar3(isOn));
            m_Toggle[3].onValueChanged.AddListener((isOn) => SetChar4(isOn));
            m_Toggle[4].onValueChanged.AddListener((isOn) => SetChar5(isOn));
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
