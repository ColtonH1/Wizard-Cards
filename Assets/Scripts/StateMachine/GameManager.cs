using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI notEnoughManaText;
    public SettingsSO settings;

    //pause menu
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject mainPauseUI;
    public GameObject optionsMenuUI;
    public GameObject instructionsMenuUI;
    public GameObject menuOptionsImagesUI;

    public TMP_Text playerName;
    public Sprite[] enemyAvatarArr;
    public Image enemyAvatar;
    //public Sprite[] playerAvatarArr;
    public Image playerAvatar;
    public GameObject audioManagerGO;
    private AudioManager audioManager;

    private void Start()
    {
        if (settings.playerName != "")
            playerName.text = settings.playerName;
        else
            playerName.text = "Player";
        SetEnemyAvatar();
        SetPlayerAvatar();
        audioManagerGO = GameObject.Find("AudioManager Variant");
        audioManager = audioManagerGO.GetComponent<AudioManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    protected virtual void SetEnemyAvatar()
    {
        enemyAvatar.sprite = settings.enemySprites[settings.enemyCharNum];
        enemyAvatar.preserveAspect = true;
    }

    protected virtual void SetPlayerAvatar()
    {
        playerAvatar.sprite = settings.playerSprites[settings.playerCharNum];
        playerAvatar.preserveAspect = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        menuOptionsImagesUI.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuOptionsImagesUI.SetActive(false);
        ResetPauseMenu();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MuteAudio()
    {
        audioManager.MuteAudio();
    }

    public void UnmuteAudio()
    {
        audioManager.UnmuteAudio();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void ShowText()
    {
        notEnoughManaText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        notEnoughManaText.gameObject.SetActive(false);
    }

    public IEnumerator ShowTextWait(float waitTime)
    {
        ShowText();
        FindObjectOfType<AudioManager>().Play("NoMana");
        yield return new WaitForSeconds(waitTime);
        HideText();
    }

    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

    public void ResetPauseMenu()
    {
        mainPauseUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        instructionsMenuUI.SetActive(false);
    }
}
