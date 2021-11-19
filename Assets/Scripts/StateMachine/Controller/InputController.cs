using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectConfirm();
        DetectCancel();
        DetectLeft();
        DetectRight();
        DetectCheatWinButton();
        DetectCheatLoseButton();
    }

    private void DetectRight()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            PressedRight?.Invoke();
        }
    }

    private void DetectLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressedLeft?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectCheatWinButton()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("3 Win Scene");
        }
    }

    private void DetectCheatLoseButton()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("4 Lose Scene");
        }
    }
}
